using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerPlace : MonoBehaviour
{
    Renderer renderer;

    GameObject tower;

    bool isNull;

    bool isActive;

    public bool ActivatePlaces { set { isActive = value; } }

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        tower = null;
        isNull = true;
    }

    private void OnMouseEnter()
    {
        if (!isActive || !isNull)
            return;

        renderer.material.color = new Color(0, 200, 255, 255);

        if (tower == null)
        {
            tower = Instantiate(TowerManager.GetTowerManager.GetTower.tower, this.gameObject.transform, false);

            tower.GetComponent<Tower>().CreateTemp();
        }
    }

    private void OnMouseExit()
    {
        if (!isActive || !isNull)
            return;

        renderer.material.color = new Color(255, 255, 255, 255);

        Destroy(tower);
    }

    private void OnMouseDown()
    {
        renderer.material.color = new Color(255, 255, 255, 255);

        TowerStruct tStruct = TowerManager.GetTowerManager.GetTower;

        tower.GetComponent<Tower>().Initialize(tStruct.price, tStruct.damage, tStruct.shootDelay, tStruct.shootSpeed);
        tower.GetComponent<Tower>().Build();

        UIManager.GetUIManager.ClickButton();

        tower = null;
        isNull = false;
    }
}
