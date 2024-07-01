using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineInternal;

public class ShooterEnemy : EnemyAttributes  ,IEnemy 
{
    public bool canshoot;
    private GameObject player;
    public Vector3 direction;
    private RaycastHit hit;
    private Rigidbody rb;
    [SerializeField] GameObject headPivot;
    [SerializeField] GameObject bodyPivot;
    [SerializeField] float radius;
    static public ShooterEnemy instance;
    private LayerMask layermask;
    void Start()
    {
        instance = this;
        rb= GetComponent<Rigidbody>();
        isAlive = true;
        player = GameObject.Find("Player");
        layermask = LayerMask.GetMask("player", "enemy");
        maxHealth = health;
    }

    void Update()
    {
       
        direction = player.transform.position - headPivot.transform.position;
        LookAtPlayer();
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
    public void Die()
    {
        rb.constraints = RigidbodyConstraints.None;
        isAlive = false;
        canvas.gameObject.SetActive(false);

        Destroy(gameObject, 5);
    }

    public void GetBodyDamage(float damage)
    {
        health -= damage;
        CheckForHealth();
    }

    public void GetHeadDamage(float damage)
    {
        health -= damage * 3;
        CheckForHealth();
    }
    private void CheckForHealth()
    {
        if (health <= 0)
        {
            if (isAlive)
            {
                UIManager.instance.UpdateScore();
            }
            Die();
        }
    }
    private void LookAtPlayer()
    {
        if(IsPlayerInRange() && isAlive && IsPlayerInSight())
        {
             canshoot = true;
             transform.LookAt(player.transform.position);
        }
        else canshoot = false;
    }
    
    private bool IsPlayerInRange()
    {
        
        if(Mathf.Sqrt(Mathf.Pow(transform.position.x-player.transform.position.x,2f) + Mathf.Pow(transform.position.z - player.transform.position.z,2f))  <= radius)
        {
            return true;
        }
        else return false;
    }
    private bool IsPlayerInSight()
    {
        //Debug.DrawRay(transform.position, direction.normalized, Color.red); 
        Physics.Raycast(headPivot.transform.position, direction.normalized , out hit, radius );
        
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Vector3 sfxdirection = -(bodyPivot.transform.position - collision.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(sfxdirection);

            Instantiate(bloodEffect, collision.contacts[0].point, rotation);
        }
    }

}
