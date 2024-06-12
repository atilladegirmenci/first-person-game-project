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

    static public UIManager instance;

    void Start()
    {
        instance = this;
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

    public void BulletCountText(bool isReloading, int bulletInMag, int MagSize)
    {
        if (!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + MagSize.ToString();

        }
        else
        {
            magTextUI.text = "RELOADING " + bulletInMag.ToString() + "/" + MagSize.ToString();
        }
    }

    public void PlayerHPTextUI()
    {
        hpTextUI.text = $"HP: {Player_health.instance.playerHealth}";
    }

    public void FlashScreenRed()
    {
        Color color = gothitEffectImage.GetComponent<Image>().color;
        color.a = 0.6f;
        gothitEffectImage.GetComponent<Image>().color = color;
    }
}
