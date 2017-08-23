using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public GameObject player;
    public GameObject states;
    public bool trigger = true;
    public float timeStamp = .5f;

    private Queue<string> sentences;
	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
    public void startDialogue(Dialogue dialogue)
    {
        player.GetComponent<PlayerController>().enabled = false;
        states.GetComponent<GameStates>().state = GameStates.GameState.CHATSTATE;
        animator.SetBool("isOpen", true);
        sentences.Clear();
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        displayNextSentence();
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    //IEnumerator performs the delay
    IEnumerator delayEnumerator(float newDelayTime)
    {
        //waits for the seconds sent from the
        //delayTime in StartCoroutine
        yield return new WaitForSeconds(newDelayTime);
        trigger = true;
        //do the logic which you want to occur 
        //after the delay
    }

    void endDialogue()
    {
        player.GetComponent<PlayerController>().enabled = true;
        animator.SetBool("isOpen", false);
        trigger = false;
        StartCoroutine(delayEnumerator(timeStamp));
        states.GetComponent<GameStates>().state = GameStates.GameState.WORLDSTATE;
    }

    void FixedUpdate()
    {
        if (Time.time >= timeStamp && Input.GetKeyDown("space"))
        {
            displayNextSentence();
        }
    }
}
