using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour {

    public enum performAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }

    public performAction battleState;
    public List<HandleTurn> list = new List<HandleTurn>();
    public List<GameObject> heroes = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public AttackScript myAction;

    public enum heroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }

    public heroGUI heroInput;

    public List<GameObject> heroesToManage = new List<GameObject>();
    private HandleTurn heroChoice;

    public GameObject ActionList;
    public GameObject gameManager;


    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
        ActionList = GameObject.Find("ActionList");
        Debug.Log(ActionList);
        battleState = performAction.WAIT;
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroes.Add(GameObject.FindGameObjectWithTag("Player"));
        heroInput = heroGUI.ACTIVATE;
        //ActionList.SetActive(false);
        myAction = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackScript>();
    }
	
	// Update is called once per frame
	void Update () {
		switch (battleState)
        {
            case (performAction.WAIT):
                if (list.Count > 0)
                {
                    battleState = performAction.TAKEACTION;
                }
                break;
            case (performAction.TAKEACTION):
                Debug.Log(list[0].Attacker);
                GameObject performer = GameObject.Find(list[0].Attacker);
                Debug.Log(performer);
                if (list[0].Type == "Enemy")
                {
                    EnemyStateMachine esm = performer.GetComponent<EnemyStateMachine>();
                    for (int i = 0; i < heroes.Count; i++)
                    {
                        if (list[0].attackersTarget == heroes[i])
                        {
                            esm.hero = list[0].attackersTarget;
                            esm.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        } /*else
                        {
                            list[0].attackersTarget = heroes[Random.Range(0, heroes.Count)];
                        }*/
                    }
                    
                }
                if (list[0].Type == "Hero")
                {
                    HeroStateMachine hsm = performer.GetComponent<HeroStateMachine>();
                    hsm.enemyToAttack = list[0].attackersTarget;
                    hsm.currentState = HeroStateMachine.TurnState.ACTION;
                }

                battleState = performAction.PERFORMACTION;
                break;
            case (performAction.CHECKALIVE):
                if (heroes.Count == 0)
                {
                    //lose game
                    Debug.Log("Lost");
                    battleState = performAction.LOSE;
                } else if (enemies.Count == 0)
                {
                    //win battle
                    Debug.Log("Win");
                    for (int i = 0; i < heroes.Count; i++)
                    {
                        heroes[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
                    }
                    battleState = performAction.WIN;
                } else
                {
                    //call function
                    //ActionList.SetActive(false);
                    gameManager.GetComponent<GameStates>().List.SetActive(false);
                    heroInput = heroGUI.ACTIVATE;
                }
                break;
            case (performAction.PERFORMACTION):

                break;
            case (performAction.LOSE):

                break;
            case (performAction.WIN):

                break;
        }

        switch (heroInput)
        {
            case (heroGUI.ACTIVATE):
                if (heroesToManage.Count > 0)
                {
                    //heroesToManage[0]
                    heroChoice = new HandleTurn();
                    //ActionList.SetActive(true);
                    gameManager.GetComponent<GameStates>().List.SetActive(true);
                    heroInput = heroGUI.WAITING;
                }
                
                break;
            case (heroGUI.WAITING):
                break;
            case (heroGUI.DONE):
                heroInputDone();
                break;
        }
	}

    public void collectActions(HandleTurn turn)
    {
        list.Add(turn);
    }

    void clearPanel()
    {

    }

    void enemyButtons()
    {

    }

    public void Input1() //attackButton
    {
        heroChoice.Attacker = heroesToManage[0].name;
        heroChoice.attacksGameObject = heroesToManage[0];
        heroChoice.Type = "Hero";
        //ActionList.SetActive(false);
        gameManager.GetComponent<GameStates>().List.SetActive(false);
        heroChoice.attackersTarget = enemies[0];
        myAction.doAttack(enemies[0]);
        heroInput = heroGUI.DONE;

    }

    void heroInputDone()
    {
        list.Add(heroChoice);
        heroesToManage.RemoveAt(0);
        heroInput = heroGUI.ACTIVATE;
    }

    public void Input2() //defendButton
    {

    }
}
