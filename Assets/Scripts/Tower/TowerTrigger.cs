using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [SerializeField]
    private Tower tower;

    GameObject curTarget;

    bool isAimed;

    private void Update()
    {
        if (!curTarget)
            isAimed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null && !isAimed)
        {
            tower.target = other.gameObject.transform;
            curTarget = other.gameObject;
            isAimed = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Enemy>() != null && !curTarget)
        {
            tower.target = other.gameObject.transform;
            curTarget = other.gameObject;
            isAimed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null && other.gameObject == curTarget)
        {
            isAimed = false;
        }
    }

}
