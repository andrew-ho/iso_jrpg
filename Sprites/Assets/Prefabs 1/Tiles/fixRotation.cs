using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixRotation : MonoBehaviour {
    Quaternion rotation;
    Vector3 pos;
    GameObject gameStates;
	// Use this for initialization
	void Start () {
        rotation = transform.rotation;
        gameStates = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = rotation;
	}

    public void OnTriggerEnter(Collider other)
    {
        
    }
    
}
