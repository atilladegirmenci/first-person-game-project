using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour , ICollectable
{
    private void Start()
    {
        Destroy(gameObject,30);
    }

    private void Update()
    {
        Rotate();

    }
    public void Collect()
    {
        if(Player_health.instance.playerHealth<100)
        {
            Player_health.instance.Heal(10);
            UIManager.instance.FlashScreen(Color.blue);
            sound_manager.instance.PlayBiteSound();
            Destroy(gameObject);
        }
        

    }
    private void Rotate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }

   
}
