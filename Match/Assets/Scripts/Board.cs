using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
	public static Board instance;
	public List<Sprite> characters = new List<Sprite>();
	public Item item;
	public int xSize, ySize;

	private Item[,] items;

    public List<Item> SelectedItems { get; set; }
    public bool IsShifting { get; set; }

    void Start()
    {
        instance = GetComponent<Board>();
        Vector2 offset = item.GetComponent<RectTransform>().sizeDelta;
        SelectedItems = new List<Item>();
        CreateBoard(offset.x, offset.y);
    }

    public void SelectItem(Item newSelected)
    {
        SelectedItems.Add(newSelected);
        refreshColors();
    }

    public void clearSelectedItems()
    {
        SelectedItems.Clear();
        refreshColors();
    }

    public void CutTailTill(Item last)
    {
        int lastIndex = SelectedItems.Count - 1;
        while(lastIndex >= 0 && SelectedItems[lastIndex] != last) {
            SelectedItems.RemoveAt(lastIndex);
            lastIndex--;
        }
        SFXManager.instance.PlaySFX(Clip.Cut);
        refreshColors();
    }

    private void refreshColors()
    {
        for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
                if(items[x, y] != null) {
                    items[x, y].changeColor();
                    items[x, y].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
        Path.instance.Clear();
        foreach(Item selected in SelectedItems){
            selected.GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            Path.instance.AddPoint(selected.Index);
        }
    }

    private void CreateBoard (float xOffset, float yOffset)
    {
        items = new Item[xSize, ySize];
        float startX = transform.position.x;
		float startY = transform.position.y;

        for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
                Item newItem = Instantiate(item, new Vector3(startX - 325 + (xOffset * x), startY - 325  + (yOffset * y), 0), item.transform.rotation);
				newItem.transform.parent = transform;
                newItem.Index = new Vector2(x, y);
				Sprite newSprite = characters[Random.Range(1, characters.Count)];
				newItem.GetComponent<Image>().sprite = newSprite;
				items[x, y] = newItem;
            }
        }
    }

    public void Match()
    {
        foreach(Item selected in SelectedItems){
            Vector2 index = selected.Index;
            items[(int)index.x, (int)index.y].FlyAway();
            items[(int)index.x, (int)index.y] = null;
        }
        SFXManager.instance.PlaySFX(Clip.Match);
        clearSelectedItems();
        ShiftBoard();
    }

    private void ShiftBoard()
    {
        IsShifting = true;
        for(int x = 0; x < xSize; x++) {
            ShiftColumn(x);
        }
    }

    private void ShiftColumn(int x)
    {
        int gapPointer = -1;
        for(int y = 0; y < ySize; y++) {
            if(items[x,y] == null || items[x,y].GetComponent<Image>().sprite == characters[0]){
                if(gapPointer == -1){
                    gapPointer = y;
                }
            }
            else if(gapPointer != -1) {
                items[x,gapPointer] = items[x,y];
                items[x,y] = null;
                gapPointer++;
            }
        }
        if(gapPointer == -1) return;
        
        float startX = transform.position.x;
		float startY = transform.position.y;
        Vector2 offset = item.GetComponent<RectTransform>().sizeDelta;

        int zz = 1;
        for(int y = gapPointer; y < ySize; y++) {
            Item newItem = Instantiate(item, new Vector3(startX - 325 + (offset.x * x), startY - 325  + (offset.y * (ySize + y - gapPointer)), 0), item.transform.rotation);
            newItem.transform.parent = transform;
            newItem.Index = new Vector2(x, y);
            Sprite newSprite = characters[Random.Range(1, characters.Count)];
            newItem.GetComponent<Image>().sprite = newSprite;
            items[x,y] = newItem;
        }
    }
}