using System;
using UnityEngine;

[Serializable]
public struct EnemyStruct
{
    public GameObject prefab;
    public int[] health;
    public int money;
    public float speed;
    public int damage;
    public GameObject hpBar;
}

[Serializable]
public struct TowerStruct
{
    public GameObject tower;
    public int price;
    public int damage;
    public float shootDelay;
    public int shootSpeed;
}