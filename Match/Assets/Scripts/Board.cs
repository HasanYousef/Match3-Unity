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
        ySize *= 2;
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
                items[x, y].changeColor();
                items[x, y].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
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
				items[x, y] = newItem;

				Sprite newSprite = characters[Random.Range(1, characters.Count)];
				newItem.GetComponent<Image>().sprite = newSprite;
				//newItem.GetComponent<Transform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }
        }
    }

    public void Match()
    {
        foreach(Item selected in SelectedItems){
            items[(int)selected.Index.x, (int)selected.Index.y].FlyAway();
        }
        SFXManager.instance.PlaySFX(Clip.Match);
        clearSelectedItems();
    }
}
