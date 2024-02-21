using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour , IEnemy
{
    Rigidbody rb;
    private float maxHealth;
    [SerializeField] private GameObject bodyPivot;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] UnityEngine.UI.Image healthbar;
    [SerializeField] Canvas canvas;
    [SerializeField] private float health;
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        maxHealth = health;
    }

    
    void Update()
    {
        canvas.transform.LookAt(GameObject.Find("Player").transform);
        healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        if (health <= 0)
        {
           StartCoroutine( Die());
        }
            
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Vector3 direction = -(bodyPivot.transform.position - collision.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
           
            Instantiate(bloodEffect, collision.contacts[0].point, rotation) ;
          
           
        }
    }

    public IEnumerator Die()
    {
        rb.freezeRotation = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    
    //public void GetDamage(float damage)
    //{
    //    health -= damage;
    //}


    public void GetHeadDamage(float damage)
    {
        health -= damage * 4;
    }

    public void GetBodyDamage(float damage)
    {
        health -= damage;
    }

   
}
