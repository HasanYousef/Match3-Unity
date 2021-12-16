using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    private Image img;
    public Vector2 Index { get; set; }
	private static Color darkColor = new Color(.7f, .7f, .7f, 1.0f);

	void Awake() {
		img = GetComponent<Image>();
		Index = new Vector2();
    }

	private void Select() {
		if(!IsHead()) {
			Board.instance.SelectItem(this);
			//SFXManager.instance.PlaySFX(Clip.Select);
		}
	}

	private void Deselect() {
		Board.instance.clearSelectedItems();
	}

	private bool IsHead() {
		List<Item> selectedItems = Board.instance.SelectedItems;
		return (selectedItems.Count > 0 && this == selectedItems[0]);
	}

	private bool InPath() {
		return Board.instance.SelectedItems.Contains(this);
	}

	public void OnClick() {
		if (Board.instance.IsShifting) return;

		if(InPath()) {
			if (IsHead()) {
				Deselect();
			} else {
				Board.instance.Match();
			}
		}
		else {
			Deselect();
			Select();
		}
	}

	public void OnHover() {
		if (Board.instance.IsShifting || IsHead()) return;
		
		List<Item> selectedItems = Board.instance.SelectedItems;
		Item tail = selectedItems[selectedItems.Count - 1];
		Vector2 dist = Index - tail.Index;
		bool isNeighbourSelected = !(dist.x > 1 || dist.x < -1 || dist.y > 1 || dist.y < -1);

		if(InPath()) {
			if(tail != this)
				Board.instance.CutTailTill(this);
		}
		else if(isNeighbourSelected && tail.GetComponent<Image>().sprite == img.sprite) {
			Board.instance.SelectItem(this);
		}
	}

	public void changeColor() 
	{
		List<Item> selectedItems = Board.instance.SelectedItems;
		bool isDark = selectedItems.Count > 0;
		foreach(Item selectedItem in selectedItems){
			if(!isDark) break;
			Vector2 dist = Index - selectedItem.Index;
			isDark = (img.sprite != selectedItem.GetComponent<Image>().sprite) 
				|| (dist.x > 1 || dist.x < -1 || dist.y > 1 || dist.y < -1);
		}
		img.color = isDark ? darkColor : Color.white;
	}

}