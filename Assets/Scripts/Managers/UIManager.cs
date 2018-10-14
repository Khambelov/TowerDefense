using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager GetUIManager;

    #region UI Params
    [SerializeField]
    private GameObject UICanvas;

    [SerializeField]
    private Text health;

    [SerializeField]
    private Text money;

    [SerializeField]
    private Text waves;

    [Header("Button")]
    [SerializeField]
    private Button tower;

    [SerializeField]
    private Image buttonFrame;

    [SerializeField]
    private Text price;

    [SerializeField]
    private Text push;

    [Header("Game Over Window")]
    [SerializeField]
    private GameObject endWindow;
    [SerializeField]
    private Text endText;
    #endregion

    public GameObject GetUICanvas { get { return UICanvas; } }

    bool btnIsActive;

    private void Awake()
    {
        GetUIManager = this;
    }

    private void Start()
    {
        btnIsActive = false;
        price.text = TowerManager.GetTowerManager.GetTower.price.ToString();
    }

    public void ClickButton()
    {
        BuyTowerActivate();
    }

    private void BuyTowerActivate()
    {
        if (!btnIsActive)
        {
            btnIsActive = true;

            buttonFrame.color = new Color(buttonFrame.color.r, buttonFrame.color.g, buttonFrame.color.b, 255f);

            TowerManager.GetTowerManager.ActivatePlaces(btnIsActive);
        }
        else
        {
            btnIsActive = false;

            buttonFrame.color = new Color(buttonFrame.color.r, buttonFrame.color.g, buttonFrame.color.b, 0f);

            TowerManager.GetTowerManager.ActivatePlaces(btnIsActive);
        }
    }

    public void UpdateHealth(int health)
    {
        this.health.text = health.ToString();
    }

    public void UpdateMoney(int money)
    {
        this.money.text = money.ToString();

        CanBuyUpdate(money);
    }

    public void UpdateWaves(int currentWave, int waveCount)
    {
        waves.text = string.Concat(currentWave, "/", waveCount);
    }

    private void CanBuyUpdate(int money)
    {
        if (money >= TowerManager.GetTowerManager.GetTower.price)
        {
            tower.interactable = true;
        }
        else
        {
            tower.interactable = false;
        }
    }

    public void GameOver(bool loose)
    {
        endWindow.SetActive(true);

        endText.text = loose ? "Вы проиграли! База была разрушена." : "Вы победили! База в безопасности.";
        endText.color = loose ? Color.red : Color.green;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator Prepare()
    {
        push.text = "Готовьтесь к новой волне";
        push.color = Color.green;

        yield return StartCoroutine(FlashText());

        yield return null;
    }

    public IEnumerator NewWave()
    {
        push.text = "Новая волна!";
        push.color = Color.red;

        yield return StartCoroutine(FlashText());

        yield return null;
    }

    private IEnumerator FlashText(bool off = false)
    {
        while (push.color.a > 0.5f)
        {
            push.color = new Color(push.color.r, push.color.g, push.color.b, push.color.a - 0.01f);

            yield return new WaitForSeconds(0.01f);
        }

        while (push.color.a < 1f)
        {
            push.color = new Color(push.color.r, push.color.g, push.color.b, push.color.a + 0.01f);

            yield return new WaitForSeconds(0.01f);
        }

        if (!off)
        {
            StartCoroutine(FlashText(true));
        }
        else
        {
            push.color = Color.clear;
        }

        yield return null;
    }
}
