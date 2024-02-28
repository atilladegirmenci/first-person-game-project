using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fast_enemy_radius : MonoBehaviour
{
    private Player_health player;

    // Start is called before the first frame update
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
        if (other.gameObject.tag == "Player" && transform.parent.GetComponent<Fast_enemy>().isAlive)
        {
            DamagePlayer();
        }

    }
    public void DamagePlayer()
    {
        player.TakeDamage(transform.parent.GetComponent<Fast_enemy>().damage);
    }
}
