using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour {

    public BaseHero hero;
    private BattleStateMachine battle;
    private float animSpeed = 5f;
    public GameObject gameManager;

    //dead
    private bool alive = true;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the progressBar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    //IEnumerator
    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPos;

    public bool battleStarted = false;

    // Use this for initialization
    void Start() {
        gameManager = GameObject.Find("GameManager");
        cur_cooldown = Random.Range(0, 2.5f);
        currentState = TurnState.PROCESSING;
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update() {
        if (battleStarted)
        {
            battle = GameObject.Find("BattleManager(Clone)").GetComponent<BattleStateMachine>();
        }
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                updateProgressBar();
                break;
            case (TurnState.ADDTOLIST):
                battle.heroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):

                break;
            case (TurnState.ACTION):
                StartCoroutine(timeForAction());
                break;
            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                } else
                {
                    Debug.Log("Hero is dead");
                    //change tag
                    this.gameObject.tag = "Dead Hero";
                    //not attackable
                    battle.heroes.Remove(this.gameObject);
                    //not manageable
                    battle.heroesToManage.Remove(this.gameObject);
                    //reset gui
                    //battle.ActionList.SetActive(false);
                    gameManager.GetComponent<GameStates>().List.SetActive(false);
                    //remove item from performList
                    if (battle.heroes.Count > 0)
                    {
                        for (int i = 0; i < battle.list.Count; i++)
                        {
                            if (battle.list[i].attacksGameObject == this.gameObject)
                            {
                                battle.list.Remove(battle.list[i]);
                            }

                            if (battle.list[i].attackersTarget == this.gameObject)
                            {
                                battle.list[i].attackersTarget = battle.heroes[Random.Range(0, battle.heroes.Count)];
                            }
                        }
                    }

                    //change color.play animation
                    //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0,0,0,0);
                    //reset hero input
                    battle.battleState = BattleStateMachine.performAction.CHECKALIVE;
                    alive = false;
                    battleStarted = false;
                }
                break;
        }
    }

    void updateProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    private IEnumerator timeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        //animate the enemy to attack hero
        Vector3 heroPos = enemyToAttack.transform.position;
        while (moveTowardsEnemy(heroPos))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(.5f);
        //do damage

        //animate to start position
        Vector3 firstPos = startPos;
        while (moveTowardsStart(firstPos))
        {
            yield return null;
        }
        //remove from list in battlestatemachine
        battle.list.RemoveAt(0);
        //reset battlestatemachine to wait
        if (battle.battleState != BattleStateMachine.performAction.WIN && battle.battleState != BattleStateMachine.performAction.LOSE)
        {
            battle.battleState = BattleStateMachine.performAction.WAIT;
            cur_cooldown = 0f;
            currentState = TurnState.PROCESSING;
        } else
        {
            currentState = TurnState.WAITING;
        }
        //end it
        actionStarted = false;
        
    }

    private bool moveTowardsEnemy(Vector3 Target)
    {
        return Target != (transform.position = Vector3.MoveTowards(transform.position, Target, animSpeed * Time.deltaTime));
    }
    private bool moveTowardsStart(Vector3 Target)
    {
        return Target != (transform.position = Vector3.MoveTowards(transform.position, Target, animSpeed * Time.deltaTime));
    }
}
