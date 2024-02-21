using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_health : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private GameObject gothitEffect;
    void Start()
    {
        
    }

    
    void Update()
    {
        hp.text = $"HP: {playerHealth}";
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (gothitEffect.GetComponent<Image>().color.a > 0)
        {
            var color = gothitEffect.GetComponent<Image>().color;
            color.a -= 0.01f;
            gothitEffect.GetComponent<Image>().color = color;
        }
    }

    public void TakeDamage(float damage)
    {
        var color = gothitEffect.GetComponent<Image>().color;
        color.a = 0.6f;
        gothitEffect.GetComponent<Image>().color = color;
        playerHealth -= damage;
    }
}
