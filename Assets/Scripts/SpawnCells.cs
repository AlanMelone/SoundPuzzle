using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCells : MonoBehaviour {

    public GameObject soundCell;
    private static int CELLS_COUNT = 5;

    private void Start()
    {
        GenerateCells();
    }

    private void GenerateCells()
    {
        List<int> randSeq = new List<int>();
        for (int i = 0; i < CELLS_COUNT; i++)
        {
            randSeq.Add(i);
        }
        for (int i = 0; i < CELLS_COUNT; i++)
        {
            

        }
        for (int i = 0; i < CELLS_COUNT; i++)
        {
            int t = Random.Range(0, randSeq.Count);
            var newObject = Instantiate(soundCell, new Vector2(-2.9f + (randSeq[t] + 0.4f*randSeq[t]), 4.1f), Quaternion.identity);
            newObject.AddComponent<SoundCellProperties>();
            newObject.AddComponent<AudioSource>();
            newObject.GetComponent<SoundCellProperties>().RightPosition = new ArrayList() { i };
            newObject.GetComponent<SoundCellProperties>().xPos = -2.9f + (randSeq[t] + 0.4f * randSeq[t]);
            newObject.GetComponent<SoundCellProperties>().yPos = 4.1f;
            newObject.GetComponent<SoundCellProperties>().Sounds = new ArrayList() { i.ToString() };
            randSeq.RemoveAt(t);
        }
    }
}
