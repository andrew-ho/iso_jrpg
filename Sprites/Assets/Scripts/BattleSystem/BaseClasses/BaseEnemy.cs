using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseClasses {

    public enum Type
    {
        HUMAN,
        PLANT,
        GHOST,
        MONSTER
    }

    public Type enemyType;


}
