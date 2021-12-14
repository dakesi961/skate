using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePfb;
    public float zSpawn = 0;
    public float tileLenght = 30;
    public int numTile = 5;

    private List<GameObject> active = new List<GameObject>();
    
    
    public Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numTile; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
                SpawnTile(Random.Range(0, tilePfb.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numTile * tileLenght))
        {
            SpawnTile(Random.Range(0, tilePfb.Length));
            DeleteTile();
        }
    }


    public void SpawnTile(int tileIndex)
    {
        GameObject go =  Instantiate(tilePfb[tileIndex], transform.forward * zSpawn, transform.rotation);
        active.Add(go);
        zSpawn += tileLenght;
    }

    private void DeleteTile()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }
}
