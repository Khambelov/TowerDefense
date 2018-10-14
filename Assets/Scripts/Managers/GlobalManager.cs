using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager GetGlobalManager;

    [SerializeField]
    private int health;

    [SerializeField]
    private int money;

    private void Awake()
    {
        GetGlobalManager = this;
        Time.timeScale = 1f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        UIManager.GetUIManager.UpdateHealth(health);

        if (health <= 0)
        {
            UIManager.GetUIManager.GameOver(true);
            Time.timeScale = 0f;
        }
    }

    public void UpdateMoney(int count)
    {
        money += count;

        UIManager.GetUIManager.UpdateMoney(money);
    }
}
