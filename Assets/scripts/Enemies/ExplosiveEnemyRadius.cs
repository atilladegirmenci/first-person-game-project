using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveEnemyRadius : MonoBehaviour
{
     private Player_health player;
    
    void Start()
    {
        player = Object.FindAnyObjectByType<Player_health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && transform.parent.GetComponent<Explosive_enemy>().isAlive)
        {
            
            transform.parent.GetComponent<Explosive_enemy>().Explode();
            gameObject.SetActive(false);
            DamagePlayer();
            Debug.Log("GAME LOST");
        }
    }
    public void  DamagePlayer()
    {
        player.TakeDamage(transform.parent.GetComponent<Explosive_enemy>().damage);
    }
}
