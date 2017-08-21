using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour {

    // Use this for initialization
    public GameObject dialogueManager;
    public Animator animator;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //IEnumerator performs the delay
    IEnumerator delayEnumerator(float newDelayTime)
    {
        //waits for the seconds sent from the
        //delayTime in StartCoroutine
        yield return new WaitForSeconds(newDelayTime);
        //do the logic which you want to occur 
        //after the delay
    }

    void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey("space") && other.gameObject.tag == "Object")
        {
            Debug.Log(other.gameObject);
            if (dialogueManager.GetComponent<DialogueManager>().trigger == true)
            {
                //other.GetComponent<PlayerController>().enabled = false;
                other.GetComponentInParent<PlayerController>().enabled = false;
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
    }
}
