using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEnemyBar : MonoBehaviour
{

    private GameObject enemy;

    [SerializeField]
    private RectTransform rect;

    [SerializeField]
    private Text hpBar;

    int hp;

    public void Initialize(GameObject enemy, int hp)
    {
        this.enemy = enemy;
        this.hp = hp;
        this.hpBar.text = this.hp.ToString();
    }

    public void UpdateHp(int hp)
    {
        this.hp = hp;
        this.hpBar.text = this.hp.ToString();
    }

    void Update()
    {
        if (enemy)
            rect.position = Camera.main.WorldToScreenPoint(enemy.transform.position);
    }
}
