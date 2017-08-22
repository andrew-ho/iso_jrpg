using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    GameObject menu;
    GameObject gameManager;
    Animator animator;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        menu = GameObject.Find("Menu");
        animator = menu.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameManager.GetComponent<GameStates>().state = GameStates.GameState.MENUSTATE;
            animator.SetBool("isOpen", true);
            
        }
    }
}
