using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject muzllePivot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootCooldown;
    private float initialCooldown;
    private bool isShooting;
    void Start()
    {
        initialCooldown = shootCooldown;
        
    }

    void Update()
    {
        ShootCooldown();
    }
    private void Shoot()
    {
        Instantiate(bullet,muzllePivot.transform.position,muzllePivot.transform.rotation, transform);
        sound_manager.instance.PlayARSound();
    }
    private void ShootCooldown()
    {
       
        if(GetComponent<ShooterEnemy>().canshoot)
        {
            if (shootCooldown <= 0 && !isShooting)
            {
                StartCoroutine(RoutineFire(1f));
                shootCooldown = initialCooldown;
            }
            else
            {
                shootCooldown -= Time.deltaTime;
            }
        }
        
    }
    private IEnumerator RoutineFire(float s)
    {
        isShooting = true;
        for (int i = 0; i < 4; i++)
        {
            Shoot();
            yield return new WaitForSeconds(s);
        }
        isShooting = false;
       

    }

}
