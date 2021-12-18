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
        lr.SetPosition(lr.positionCount - 1, new Vector3((newPosition.x * 1.12f) - 9.9f, (newPosition.y * 1.12f) - 8.8f, 0f));
    }

    public void Clear()
    {
        lr.positionCount = 0;
    }
}
