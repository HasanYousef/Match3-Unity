using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardContainer : MonoBehaviour
{
    private bool IsShowingUp;
    private Vector3 OriginalPosition;

    void Start()
    {
        IsShowingUp = true;
        OriginalPosition = transform.position;
        transform.position -= new Vector3(0, 720, 0);

    }

    void Update()
    {
        if(IsShowingUp){
            if(OriginalPosition.y <= transform.position.y)
                IsShowingUp = false;
            else{
                Vector3 dir = (OriginalPosition - transform.position).normalized;
                float dist = OriginalPosition.y - transform.position.y;
                float origDist = OriginalPosition.y - 720;
                transform.position += dir * (dist * 5) * Time.deltaTime;
            }
        }
    }
}
