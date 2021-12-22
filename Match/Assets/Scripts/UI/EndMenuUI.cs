using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuUI : MonoBehaviour
{
	public static EndMenuUI instance;

	void Start () {
		instance = GetComponent<EndMenuUI>();
        this.name = "EndMenuUI";
    }

    public void Leave(){
        Object.Destroy(this.gameObject);
    }
}
