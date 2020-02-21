﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public Sprite sprite;
    public int maxHP;
    public float regularSpeed;
    public float enragedSpeed;
    public int waypointPauseTime;
    public int attackTime;

}
