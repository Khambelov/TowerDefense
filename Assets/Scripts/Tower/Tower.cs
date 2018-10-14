using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region GameObjects Param
    [Header("Gun")]
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private Transform gunZone;

    [SerializeField]
    private Renderer renderGun;

    [SerializeField]
    private Collider colliderGun;

    [Header("Tower")]
    [SerializeField]
    private Renderer renderTower;

    [SerializeField]
    private Collider colliderTower;

    [Header("Bullet")]
    [SerializeField]
    private GameObject bullet;

    [Header("TriggerZone")]
    [SerializeField]
    private Collider colliderTrigger;
    #endregion

    public Transform target;

    private int price;
    public int GetPrice { get { return price; } }
    private int damage;
    public int GetDamage { get { return damage; } }
    private float shootDelay;
    public float GetShootDelay { get { return shootDelay; } }
    private int shootSpeed;
    public int GetShootSpeed { get { return shootSpeed; } }

    bool isTemp;
    bool isShoot;

    public void Initialize(int price, int damage, float shootDelay, int shootSpeed)
    {
        this.price = price;
        this.damage = damage;
        this.shootDelay = shootDelay;
        this.shootSpeed = shootSpeed;
    }

    void Update()
    {
        if (!isTemp)
        {
            if (target)
            {
                gunZone.LookAt(target);

                if (!isShoot)
                    StartCoroutine(Shoot());
            }
        }
    }

    public void CreateTemp()
    {
        isTemp = true;

        colliderTower.enabled = false;
        colliderGun.enabled = false;
        colliderTrigger.enabled = false;
        renderGun.material.color = new Color(renderGun.material.color.r, renderGun.material.color.g, renderGun.material.color.b, 0.5f);
        renderTower.material.color = new Color(renderTower.material.color.r, renderTower.material.color.g, renderTower.material.color.b, 0.5f);
    }

    public void Build()
    {
        isTemp = false;

        colliderTower.enabled = true;
        colliderGun.enabled = true;
        colliderTrigger.enabled = true;
        renderGun.material.color = new Color(renderGun.material.color.r, renderGun.material.color.g, renderGun.material.color.b, 1f);
        renderTower.material.color = new Color(renderTower.material.color.r, renderTower.material.color.g, renderTower.material.color.b, 1f);
        GlobalManager.GetGlobalManager.UpdateMoney(-price);
    }

    IEnumerator Shoot()
    {
        isShoot = true;

        yield return new WaitForSeconds(shootDelay);

        GameObject obj = Instantiate(this.bullet, gun.transform.position, Quaternion.identity);
        obj.GetComponent<Bullet>().Initialize(target, shootSpeed, damage);

        isShoot = false;
    }
}
