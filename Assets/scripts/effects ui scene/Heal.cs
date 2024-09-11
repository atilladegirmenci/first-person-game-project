using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : CollectableBase
{
    [SerializeField] private int healAmount;
    public override void Collected()
    {

        if (Player_health.instance.playerHealth < 100)
        {
            Player_health.instance.Heal(healAmount);
            UIManager.instance.StartCoroutine(UIManager.instance.CollectedItemOnUI(healAmount.ToString(), "Heal"));
            UIManager.instance.FlashScreen(Color.blue);
            sound_manager.instance.PlayBiteSound();
            Destroy(gameObject);
        }
    }
   
  

   
}
