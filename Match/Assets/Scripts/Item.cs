using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	private Color darkColor = new Color(.7f, .7f, .7f, 1.0f);
	[SerializeField]
	private float FlyAwaySpeed;
	[SerializeField]
	private Vector3 FlyingDestination;
	public Vector3 ShiftDestination { get; set; }

    private Image img;
    public Vector2 Index { get; set; }

	public bool IsFlying { get; set; } = false;

	void Awake() {
		img = GetComponent<Image>();
		Index = new Vector2();
    }

	void Update() {
		if(transform.position.y > FlyingDestination.y)
			IsFlying = false;
		if(IsFlying){
			Vector3 dir = (FlyingDestination - transform.position).normalized;
			float dist = FlyingDestination.y - transform.position.y;
			transform.position += dir * (dist * FlyAwaySpeed) * Time.deltaTime;
		}
    }

	private void Select() {
		if(!IsHead()) {
			Board.instance.SelectItem(this);
			SFXManager.instance.PlaySFX(Clip.Select);
		}
	}

	private void Deselect() {
		Board.instance.clearSelectedItems();
		SFXManager.instance.PlaySFX(Clip.Select);
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
		if (Board.instance.IsShifting) return;
		
		List<Item> selectedItems = Board.instance.SelectedItems;
		if (selectedItems.Count == 0) return;
		Item tail = selectedItems[selectedItems.Count - 1];
		Vector2 dist = Index - tail.Index;
		bool isNeighbourSelected = !(dist.x > 1 || dist.x < -1 || dist.y > 1 || dist.y < -1);

		if(InPath()) {
			Board.instance.CutTailTill(this);
		}
		else if(isNeighbourSelected && tail.GetComponent<Image>().sprite == img.sprite) {
			Board.instance.SelectItem(this);
			SFXManager.instance.PlaySFX(Clip.Hover);
		}
	}

	public void changeColor() 
	{
		if(IsFlying) {
			img.color = Color.white;
			return;
		}
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

	public void FlyAway()
	{
		transform.parent = GameObject.Find("Canvas").GetComponent<Transform>();
		IsFlying = true;
	}

}