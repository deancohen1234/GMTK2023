using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScroller : MonoBehaviour
{
    public float terrainHeight = 5f;
    public GameObject[] terrainsPrefabs;
    public Transform startPoint;

    public float scrollSpeed = 20f;
    public float scrollCutoffZ = -20f;

    private GameObject[] instantiatedTerrains;

    // Start is called before the first frame update
    void Start()
    {
        instantiatedTerrains = new GameObject[terrainsPrefabs.Length];

        for (int i = 0; i < terrainsPrefabs.Length; i++)
        {
            GameObject newTerrain = Instantiate(terrainsPrefabs[i]);

            instantiatedTerrains[i] = newTerrain;

            Vector3 position = startPoint.position + (Vector3.forward * terrainHeight * i);
            instantiatedTerrains[i].transform.position = position;
        }

        //organize them in a vertical (z) row
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < instantiatedTerrains.Length; i++)
        {
            Vector3 newPos = instantiatedTerrains[i].transform.position + Vector3.forward * scrollSpeed * Time.deltaTime;

            if (newPos.z >= scrollCutoffZ)
            {
                //move to top
                int topIndex = (int)Mathf.Repeat(i - 1, instantiatedTerrains.Length);

                Vector3 position = instantiatedTerrains[topIndex].transform.position - (Vector3.forward * terrainHeight);
                instantiatedTerrains[i].transform.position = position;
            }
            else
            {
                instantiatedTerrains[i].transform.position = newPos;
            }

        }
    }
}
