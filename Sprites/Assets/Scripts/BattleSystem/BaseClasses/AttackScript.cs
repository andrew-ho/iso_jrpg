using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackScript : MonoBehaviour {
    public List<string> attacks = new List<string>();
    Skills skill = new Skills();

    public void doAttack(GameObject target)
    {
        string attack = attacks[Random.Range(0, attacks.Count)];
        Debug.Log(attack);
        skill.doSomething(attack, target);
    }
}
