using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            GlobalManager.GetGlobalManager.TakeDamage(other.gameObject.GetComponent<Enemy>().GetDamage);
        }

        Destroy(other.gameObject);
    }
}
