using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int colums = 8;
    public int rows = 8;

    public Count wallCount = new Count(5, 9);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPosition = new List<Vector3>();

    void initializePosition()
    {
        gridPosition.Clear();
        for (int i = 1; i < colums; i++)
        {
            for (int k = 1; k < rows; k++)
            {
                gridPosition.Add(new Vector3(i, k, 0f));
            }
        }
    }

    void boardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int i = -1; i < colums; i++)
        {
            for (int k = -1; k < rows; k++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (i == -1  || i == colums || k == -1 || k == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, k, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    Vector3 randomPosition()
    {
        int randomIndex = Random.Range(0, gridPosition.Count);
        Vector3 randomPosition = gridPosition[randomIndex];
        gridPosition.RemoveAt(randomIndex);
        return randomPosition;
    }

    void layoutObjectAtRandom(GameObject[] tileArray, int min, int max)
    {
        int objectCount = Random.Range(min, max + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randPosition = randomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randPosition, Quaternion.identity);

        }
    }

    public void setUpScene(int level)
    {
        boardSetup();
        initializePosition();
        layoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
