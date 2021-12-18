using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	public static InGameUI instance;

	void Start () {
		instance = GetComponent<InGameUI>();
        this.name = "InGameUI";
        SFXManager.instance.PlaySFX(Clip.StartGame);
    }
}
