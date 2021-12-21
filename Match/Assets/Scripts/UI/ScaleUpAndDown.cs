using UnityEngine;
using System.Collections;    

public class ScaleUpAndDown: MonoBehaviour {

    public float scalingTime, scalingSpeed = 0.3f, targetScale = 0.3f;
    public float length = 0.3f;

    void Update ()
    {
        scalingTime = Time.time * scalingSpeed;  
        transform.localScale = new Vector3 (
            Mathf.PingPong (scalingTime, length) + targetScale, 
            Mathf.PingPong (scalingTime, length) + targetScale, 0
        );
    }
}