using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGrid : MonoBehaviour {

    public List<GameObject> tileGridRow = new List<GameObject>();
    public List<GameObject> tileGridColumn = new List<GameObject>();
    public GameObject[,] tileGrid;
    public int columns;
    public int rows;
    int count;

	// Use this for initialization
	void Start () {
        count = 0;
        GameObject s = new GameObject();
        tileGrid = new GameObject[rows, columns];
		for (int i = 0; i < tileGridRow.Count; i++)
        {
            for (int k = 0; k < tileGridColumn.Count; k++)
            {
                GameObject go = Instantiate(tileGridRow[i], new Vector3(i, 0, k), Quaternion.identity);
                count++;
                go.name = go.name + count;
                go.transform.parent = s.transform;
                tileGrid[i, k] = go;
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
