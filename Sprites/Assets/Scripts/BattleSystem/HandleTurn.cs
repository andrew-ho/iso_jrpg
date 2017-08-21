using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn {
    public string Attacker;
    public string Type;

    public GameObject attacksGameObject;//who attacks

    public GameObject attackersTarget;//who is being attacked

    //attack
    public AttackScript attack;
}
