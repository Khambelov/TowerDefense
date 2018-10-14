using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    public static TowerManager GetTowerManager;

    [SerializeField]
    private TowerStruct tower;

    public TowerStruct GetTower { get { return tower; } }

    public List<GameObject> towerPlaces;

    void Awake()
    {
        GetTowerManager = this;
    }

    public void ActivatePlaces(bool isActive)
    {
        if (!isActive)
        {
            foreach (GameObject place in towerPlaces)
            {
                place.GetComponent<TowerPlace>().ActivatePlaces = false;
            }
        }
        else
        {
            foreach (GameObject place in towerPlaces)
            {
                place.GetComponent<TowerPlace>().ActivatePlaces = true;
            }
        }
    }
}