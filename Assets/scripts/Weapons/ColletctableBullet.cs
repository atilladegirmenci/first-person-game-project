using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletctableBullet : CollectableBase
{
    private CollectableTypes collectedBullet;
    protected override void Update()
    {
        base.Update();
    }


    public override void Collected()
    {

        switch (type)

        {
            case CollectableTypes.PistolAmmo:
                int amount1 = Random.Range(5, 20);
                Pistol.instance.UpdateBullet(amount1);
                UIManager.instance.StartCoroutine(UIManager.instance.CollectedItemOnUI(amount1.ToString(), "Pistol Ammo"));  //MonoBehaviour bulunmayan 
                break;                                                                                                       //classlarda startCoroutine çalışmıyor    
            case CollectableTypes.ShotgunAmmo:
                int amount2 = Random.Range(1, 5);
                Shotgun.instance.UpdateBullet(amount2);
                UIManager.instance.StartCoroutine(UIManager.instance.CollectedItemOnUI(amount2.ToString(), "Shotgun Ammo")); 

                break;
            case CollectableTypes.RifleAmmo:
                int amount3 = Random.Range(10, 26);
                Assault_rifle.instance.UpdateBullet(amount3);
                UIManager.instance.StartCoroutine(UIManager.instance.CollectedItemOnUI(amount3.ToString(), "AR Ammo")); 
                break;
            default:
                break;


        }

        sound_manager.instance.PlayAmmoCollectSound();
        Destroy(gameObject);
    }

    

}
