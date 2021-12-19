using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	private Color darkColor = new Color(.7f, .7f, .7f, 1.0f);
	[SerializeField]
	private float FlyAwaySpeed;
	[SerializeField]
	private float ShiftingSpeed;
	[SerializeField]
	private Vector3 FlyingDestination;
	private float YShiftDestination;

    private Image img;
    public Vector2 Index { get; set; }

	public bool IsFlying { get; set; } = false;
	private Vector3 OriginalPosition;
	public bool IsShifting { get; set; } = false;

	void Awake() {
		img = GetComponent<Image>();
		Index = new Vector2();
    }

	void Update() {
		if(IsFlying){
			if(transform.position.y > FlyingDestination.y - 80){
				Object.Destroy(this.gameObject);
			}
			else{
				Vector3 dir = (FlyingDestination - transform.position).normalized;
				float dist = FlyingDestination.y - transform.position.y;
				float origDist = FlyingDestination.y - OriginalPosition.y;
				transform.position += dir * (dist * FlyAwaySpeed) * Time.deltaTime;
				float scale = dist / origDist + 0.2f;
				transform.localScale = new Vector3(scale, scale, scale);
			}
		}
		if(IsShifting){
			if(YShiftDestination >= transform.position.y){
				IsShifting = false;
				Board.instance.ItemFinishedShifting();
			}
			else {
				transform.position -= new Vector3(0, 300 * ShiftingSpeed * Time.deltaTime, 0);
			}
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
			if(IsHead()) {
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
			isDark = (img.sprite != selectedItem.GetComponent<Image>().sprite);
		}
		img.color = (isDark || Board.instance.CheckIfDisabled()) ? darkColor : Color.white;
		transform.localScale = isDark ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(1f, 1f, 1f);
	}

	public void FlyAway()
	{
		transform.parent = GameObject.Find("InGameUI").GetComponent<Transform>();
		IsFlying = true;
		OriginalPosition = transform.position;
	}

	public bool SetYShiftDestination(float y)
	{
		if(y != transform.position.y){
			IsShifting = true;
			YShiftDestination = y;
		}
		return IsShifting;
	}
}