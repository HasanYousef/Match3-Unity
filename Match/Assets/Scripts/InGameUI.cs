using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
	public static InGameUI instance;

	void Start () {
		instance = GetComponent<InGameUI>();
        this.name = "InGameUI";
    }

    public void UpdateEnemyHealth(int newHealth){

    }

    public void UpdatePlayerHealth(int newHealth){
        
    }
}
