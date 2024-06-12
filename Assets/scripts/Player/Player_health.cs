using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_health : MonoBehaviour
{
    [SerializeField] public float playerHealth;
    static public Player_health instance; 

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        //UIManager.instance.HPTextUI(playerHealth);

        if (playerHealth <= 0)
        {
           StartCoroutine(MySceneManager.instance.OpenDeathScene()); 
        }
       
    }

    public void TakeDamage(float damage)
    {
        UIManager.instance.FlashScreenRed();
        playerHealth -= damage;
    }
}
