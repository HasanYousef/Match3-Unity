using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
	public StreetChunk[] Chunks;
    [SerializeField] private int ChunksPerLoad;
    private int ChunksCount = 0;
    
    void Start()
    {
        LoadChunks();
    }

    void Update()
    {
        
    }

    public void LoadChunks()
    {
        for(int i = 0; i < ChunksPerLoad; i++) {
            StreetChunk newChunk = Instantiate(Chunks[i % Chunks.Length], new Vector3((i + ChunksCount) * 12, 0, 0), Chunks[i % Chunks.Length].transform.rotation);
            newChunk.transform.parent = transform;
        }
    }
}
