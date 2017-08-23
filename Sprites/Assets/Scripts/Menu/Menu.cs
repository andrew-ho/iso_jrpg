using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    GameObject menu;
    GameObject gameManager;
    Animator animator;
    GameObject Hp;
    GameObject player;
    GameObject itemText;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        menu = GameObject.Find("Menu");
        animator = menu.GetComponent<Animator>();
        Hp = GameObject.Find("HP Bar");
        player = GameObject.FindGameObjectWithTag("Player");
        itemText = GameObject.Find("ItemText");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameManager.GetComponent<GameStates>().state = GameStates.GameState.MENUSTATE;
            if (!animator.GetBool("isOpen"))
            {
                player.GetComponent<PlayerController>().enabled = false;
                animator.SetBool("isOpen", true);
                Hp.SetActive(false);
            }
            else
            {
                animator.SetBool("isOpen", false);
                Hp.SetActive(true);
                player.GetComponent<PlayerController>().enabled = true;
            }
            
        }
    }

    public void getItemDescription()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            GameObject item = hit.transform.gameObject;
            Debug.Log(item.GetComponent<Item>());
        }
    }
}
