using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills {
    public string attackName;
    public string attackDescription;
    public float attackDamage;//Damage
    public float attackCost; //Mana cost

    public void Slash()
    {
        attackName = "Slash";
        attackDescription = "Basic attack";
        attackDamage = 10f;
        attackCost = 0f;
    }

    public void hammerSwing()
    {
        attackName = "Hammer Swing";
        attackDescription = "Attack with your hammer";
        attackDamage = 15f;
        attackCost = 0f;
    }

    public void doSomething(string attack, GameObject target)
    {
        if (target == null)
        {
            Debug.Log("Its null");
        }
        switch (attack.ToLower())
        {
            case ("slash"):
                Slash();
                if (target.tag == "Player")
                {
                    float hp = target.GetComponent<HeroStateMachine>().hero.curHp;
                    target.GetComponent<HeroStateMachine>().hero.curHp = hp - attackDamage;
                    if (target.GetComponent<HeroStateMachine>().hero.curHp <= 0)
                    {
                        target.GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.DEAD;
                    }
                } else
                {
                    float hp = target.GetComponent<EnemyStateMachine>().enemy.curHp;
                    target.GetComponent<EnemyStateMachine>().enemy.curHp = hp - attackDamage;
                    if (target.GetComponent<EnemyStateMachine>().enemy.curHp <= 0)
                    {
                        target.GetComponent<EnemyStateMachine>().currentState = EnemyStateMachine.TurnState.DEAD;
                    }
                }
                break;

            case ("hammer swing"):
                hammerSwing();
                break;
        }
    }
}
