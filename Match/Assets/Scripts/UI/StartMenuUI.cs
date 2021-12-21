using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
	public static StartMenuUI instance;

	void Start () {
		instance = GetComponent<StartMenuUI>();
        this.name = "StartMenuUI";
    }

    public void Leave(){
        Object.Destroy(this.gameObject);
    }
}
