using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fast_enemy : EnemyAttributes , IEnemy
{

    private GameObject player;
    private Rigidbody rb;
    private float cooldownReset;
    public float damage;
    [SerializeField] private AudioSource  moveSound;
    [SerializeField] private float moveCooldown;
    [SerializeField] private float radius;
    [SerializeField] private GameObject bodyPivot;
    
    void Start()
    {
        cooldownReset = moveCooldown;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        maxHealth = health;
        isAlive = true; 
    }

    void Update()
    {
        if(isAlive) transform.LookAt(player.transform);
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        
        MoveTowardsPlayer();
    }
    private void MoveTowardsPlayer()
    { 
        if(moveCooldown<=0 && isAlive)
        {
           transform.position = Vector3.MoveTowards(transform.position, transform.position + FindRandomPos(), 100);
          
            
            moveCooldown = cooldownReset;
            moveSound.Play();
        }
        else
        {
            moveCooldown -= Time.deltaTime;
        }
    }
    private Vector3 FindRandomPos()
    {
        Vector3 pos;
        float dirx;
        float dirz;

        dirx = transform.position.x - player.transform.position.x;
        dirz =transform.position.z - player.transform.position.z;
        pos = new Vector3( Mathf.Clamp(-dirx,-1.3f,1.3f)*radius*Random.Range(0.5f,1f) , 0 , Mathf.Clamp(-dirz, -1.3f, 1.3f)*radius*Random.Range(0.5f, 1f));
        return pos; 
    }
    public void Die()
    {
        rb.freezeRotation = false;
        isAlive = false;
        canvas.gameObject.SetActive(false);
        CollectableSpawner.Instance.SpawnHeal(5, transform.position);

        Destroy(gameObject,5);
    }

    public void GetBodyDamage(float damage)
    {
        health -= damage;
        CheckForHealth();
    }

    public void GetHeadDamage(float damage)
    {
        health -= damage*3;
        CheckForHealth();
    }
    private void CheckForHealth()
    {
        if (health <= 0)
        {
            if(isAlive)
            {
                UIManager.instance.UpdateScore();
            }
            Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Vector3 direction = -(bodyPivot.transform.position - collision.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Instantiate(bloodEffect, collision.contacts[0].point, rotation);
        }
    }

}
