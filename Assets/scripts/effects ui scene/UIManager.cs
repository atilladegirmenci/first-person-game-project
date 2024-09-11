using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int score;
    [SerializeField] private TextMeshProUGUI magTextUI;
    [SerializeField] private TextMeshProUGUI hpTextUI;
    [SerializeField] private TextMeshProUGUI scoreTextUI;
    [SerializeField] private GameObject gothitEffectImage;
    [SerializeField] private TextMeshProUGUI collectedNameTextUI;
    [SerializeField] private TextMeshProUGUI collectedAmountTextUI;

    static public UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
        score = 0;
    }


    void Update()
    {
        PlayerHPTextUI();
        scoreTextUI.text = score.ToString();

        if (gothitEffectImage.GetComponent<Image>().color.a > 0 && Player_health.instance.playerHealth > 0)
        {
            Color color = gothitEffectImage.GetComponent<Image>().color;
            color.a -= 0.01f;
            gothitEffectImage.GetComponent<Image>().color = color;
        }
    }
    public void UpdateScore()
    {
        score++;
        return;
    }

    public void BulletCountText(bool isReloading, int bulletInMag, int reservebullet)
    {
        if (!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + reservebullet.ToString();

        }
        else
        {
            magTextUI.text = "RELOADING " + bulletInMag.ToString() + "/" + reservebullet.ToString();
        }
    }

    public void PlayerHPTextUI()
    {
        hpTextUI.text = $"HP: {Player_health.instance.playerHealth}";
    }

    public IEnumerator CollectedItemOnUI(string amount, string name)
    {
       
        collectedAmountTextUI.text ="+" + amount;
        collectedNameTextUI.text = name;

        collectedNameTextUI.gameObject.SetActive(true);
        collectedAmountTextUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        collectedNameTextUI.gameObject.SetActive(false);
        collectedAmountTextUI.gameObject.SetActive(false);
    }


    public void FlashScreen(Color choosenColor)
    {
        Color color = gothitEffectImage.GetComponent<Image>().color;
        color = choosenColor;
        color.a = 0.6f;
        gothitEffectImage.GetComponent<Image>().color = color;
    }
}
