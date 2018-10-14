using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private int speed;
    private int damage;

    public void Initialize(Transform target, int speed, int damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }

    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        else
            Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
