using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {

    public BaseEnemy enemy;
    private BattleStateMachine battle;
    public GameObject hero;

    public AttackScript myAction;

    private bool alive = true;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for the progressBar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    private Vector3 startPos;
    //IENUM stuff
    private bool actionStarted = false;

    private float animSpeed = 5f;



    // Use this for initialization
    void Start () {
        startPos = transform.position;
        myAction = this.GetComponent<AttackScript>();
        currentState = TurnState.PROCESSING;
        battle = GameObject.Find("BattleManager(Clone)").GetComponent<BattleStateMachine>();
        this.GetComponent<CameraFacingBillboard>();
        
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                if (battle.heroInput == BattleStateMachine.heroGUI.WAITING)
                {
                    return;
                }
                else
                {
                    updateProgressBar();
                }
                break;
            case (TurnState.CHOOSEACTION):
                chooseAction();
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
                    //change tag
                    this.gameObject.tag = "Dead enemy";
                    //not attackable
                    battle.enemies.Remove(this.gameObject);
                    //remove from performlist
                    if (battle.enemies.Count > 0)
                    {
                        for (int i = 0; i < battle.list.Count; i++)
                        {
                            if (battle.list[i].attacksGameObject == this.gameObject)
                            {
                                battle.list.Remove(battle.list[i]);
                            }
                            if (battle.list[i].attackersTarget == this.gameObject)
                            {
                                battle.list[i].attackersTarget = battle.enemies[Random.Range(0, battle.enemies.Count)];
                            }
                        }
                    }
                    alive = false;

                    battle.battleState = BattleStateMachine.performAction.CHECKALIVE;
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
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void chooseAction()
    {
        if (battle.heroes.Count == 0)
        {
            return;
        }   
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = enemy.nameOfObject;
        myAttack.Type = "Enemy";
        myAttack.attacksGameObject = this.gameObject;
        myAttack.attackersTarget = battle.heroes[0];
        battle.collectActions(myAttack);
    }

    private IEnumerator timeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        //animate the enemy to attack hero
        Vector3 heroPos = hero.transform.position;
        while (moveTowardsEnemy(heroPos))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(.5f);
        //do damage
        myAction.doAttack(battle.heroes[0]);
        //animate to start position
        Vector3 firstPos = startPos;
        while (moveTowardsStart(firstPos))
        {
            yield return null;
        }
        //remove from list in battlestatemachine
        battle.list.RemoveAt(0);
        //reset battlestatemachine to wait
        battle.battleState = BattleStateMachine.performAction.WAIT;
        //end it
        actionStarted = false;
        cur_cooldown = 0f;
        currentState = TurnState.PROCESSING;
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
