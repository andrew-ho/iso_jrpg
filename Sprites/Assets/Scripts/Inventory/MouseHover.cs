using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler {

    GameObject panel;

    public void OnPointerEnter(PointerEventData data)
    {
        //GameObject parent = this.transform.parent.gameObject;
        //panel = parent.transform.Find("ActivePanel").gameObject;
        Debug.Log(data);
        panel = GameObject.Find("ActivePanel");
        Debug.Log(panel);
        panel.SetActive(false);

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
