using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveEnemyRadius : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.parent.GetComponent<Explosive_enemy>().isAlive)
        {
            transform.parent.GetComponent<Explosive_enemy>().Explode();
            Player_controller.instance.PushBack(new Vector3(transform.position.x - Player_controller.instance.transform.position.x, 0.2f, Player_controller.instance.transform.position.z - transform.position.z), 1000);
            gameObject.SetActive(false);
            DamagePlayer();
        }
    }
    public void  DamagePlayer()
    {
       Player_health.instance.TakeDamage(transform.parent.GetComponent<Explosive_enemy>().damage);
    }
}
