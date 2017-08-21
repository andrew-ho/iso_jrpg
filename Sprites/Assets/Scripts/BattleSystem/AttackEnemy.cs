using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour {
    public GameObject enemy;
    BattleStateMachine bsm;

    public void attackEnemy()
    {
        bsm = GameObject.Find("BattleManager(Clone)").GetComponent<BattleStateMachine>();
        bsm.Input1();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
