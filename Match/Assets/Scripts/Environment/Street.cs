using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
	public StreetChunk[] Chunks;
	private List<StreetChunk> LoadedChunks;
    [SerializeField] private int ChunksPerLoad;

    void Start(){
        LoadedChunks = new List<StreetChunk>();
        AddChunk(0);
    }

    public void LoadChunks()
    {
        for(int i = 0; i < ChunksPerLoad; i++) {
            AddChunk(i % Chunks.Length);
        }
    }

    private void AddChunk(int type){
        StreetChunk newChunk = Instantiate(Chunks[type], new Vector3(LoadedChunks.Count * 12, 0, 0), transform.parent.rotation);
        newChunk.transform.parent = transform;
        LoadedChunks.Add(newChunk);
    }
}
