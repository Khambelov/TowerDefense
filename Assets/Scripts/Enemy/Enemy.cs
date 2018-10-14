using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    public int GetHealth { get { return health; } }
    private int money;
    public int GetMoney { get { return money; } }
    private float speed;
    public float GetSpeed { get { return speed; } }
    private int damage;
    public int GetDamage { get { return damage; } }
    private GameObject hpBar;
    public GameObject GetHpBar { get { return hpBar; } }

    private List<Transform> movePoints;

    private int pointIndex;

    void Start()
    {
        pointIndex = 0;
        movePoints = SpawnEnemy.GetSpawnEnemy.MovePoints;
    }

    public void Initialize(int[] health, int money, float speed, int damage, GameObject hpBar)
    {
        this.health = health[Random.Range(0, health.Length)];
        this.money = money;
        this.speed = speed;
        this.damage = damage;
        this.hpBar = hpBar;

        this.hpBar.GetComponent<HpEnemyBar>().Initialize(this.gameObject, this.health);
    }

    // Update is called once per frame
    void Update()
    {
        if (pointIndex < movePoints.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex].position, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, movePoints[pointIndex].position) < 0.1f)
            {
                if (movePoints[pointIndex].gameObject.GetComponent<ControlPoint>() != null)
                {
                    if (movePoints[pointIndex].gameObject.GetComponent<ControlPoint>().CheckPoint)
                    {
                        StartCoroutine(RotateUp());
                    }
                    else
                    {
                        StartCoroutine(RotateDown());
                    }
                }

                pointIndex++;
            }
        }
    }

    IEnumerator RotateUp()
    {
        float angle = 0f;

        while (angle > -90f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 5f, transform.eulerAngles.z);
            angle -= 5f;

            yield return new WaitForSeconds(0.001f);
        }


        yield return null;
    }

    IEnumerator RotateDown()
    {
        float angle = 0f;

        while (angle < 90f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 5f, transform.eulerAngles.z);
            angle += 5f;

            yield return new WaitForSeconds(0.001f);
        }

        yield return null;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        hpBar.GetComponent<HpEnemyBar>().UpdateHp(health);

        if (health <= 0)
        {
            GlobalManager.GetGlobalManager.UpdateMoney(money);

            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        SpawnEnemy.GetSpawnEnemy.CheckWave();
        Destroy(hpBar);
    }
}
