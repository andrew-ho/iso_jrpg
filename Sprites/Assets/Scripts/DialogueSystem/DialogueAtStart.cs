using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAtStart : MonoBehaviour {
    public GameObject dialogueManager;
    public GameObject player;
    public GameObject gameManager;

    // Use this for initialization
    void Start () {
        player.GetComponentInParent<PlayerController>().enabled = false;
        GetComponent<DialogueTrigger>().TriggerDialogue();
        Destroy(this.gameObject);
    }
    IEnumerator delayEnumerator(float newDelayTime)
    {
        //waits for the seconds sent from the
        //delayTime in StartCoroutine
        yield return new WaitForSeconds(newDelayTime);
        //do the logic which you want to occur 
        //after the delay
    }
    // Update is called once per frame
    void Update () {
		
	}
}
