using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
	public static Path instance;

    private LineRenderer lr;
    
    void Start()
    {
        instance = GetComponent<Path>();
    }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void AddPoint(Vector3 newPosition)
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, new Vector3((newPosition.x / 1.14f) - 2.15f, (newPosition.y / 1.14f) - 4.1f, 0));
    }

    public void Clear()
    {
        lr.positionCount = 0;
    }
}
