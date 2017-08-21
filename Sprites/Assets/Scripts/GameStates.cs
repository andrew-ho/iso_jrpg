using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    Rigidbody rb;
    GameObject player;

    GameState state;
    public GameObject BattleManager;
    private bool created = false;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public GameObject List;

    RaycastHit hit;
    Ray ray;

    public enum GameState
    {
        WORLDSTATE,
        BATTLESTATE
    }

    void randomEncounter(Rigidbody player)
    {
        if (player.velocity.magnitude > 0)
        {
            if (Random.Range(0, 10000) < 10)
            {
                Debug.Log("Attacked!");
                state = GameState.BATTLESTATE;
            }
        }
    }

	// Use this for initialization
	void Start () {
        List = GameObject.Find("ActionList");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        List.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        randomEncounter(rb);
        switch (state)
        {
            case (GameState.BATTLESTATE):
                if (!created)
                {
                    ray = Camera.main.ScreenPointToRay(GameObject.Find("Checker").transform.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                    }
                    //ActionList.SetActive(false);
                    player.GetComponent<HeroStateMachine>().enabled = true;
                    player.GetComponent<HeroStateMachine>().battleStarted = true;
                    player.GetComponent<PlayerController>().enabled = false;
                    created = true;
                    GameObject bm = Instantiate(Resources.Load("Prefabs/BattleManager"), Vector3.zero, Quaternion.identity) as GameObject;
                    GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], Vector3.zero, Quaternion.identity) as GameObject;
                }
                break;
            case (GameState.WORLDSTATE):
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<HeroStateMachine>().enabled = false;
                break;
        }
	}
}
