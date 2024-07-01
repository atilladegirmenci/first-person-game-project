using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private ShooterEnemy shooterEnemy;
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damage;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        BulletTravel();
    }

    private void BulletTravel()
    {
        rb.AddForce(transform.parent.GetComponent<ShooterEnemy>().direction.normalized * bulletSpeed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player" && ShooterEnemy.instance.isAlive)
        {
            DamagePlayer();
        }
        Destroy(gameObject);
    }

    private void DamagePlayer()
    {
        Player_health.instance.TakeDamage(damage);
    }
   
}
