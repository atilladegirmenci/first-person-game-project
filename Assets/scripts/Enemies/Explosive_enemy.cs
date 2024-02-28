using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_enemy : MonoBehaviour , IEnemy
{
    Rigidbody rb;
    private float maxHealth;

    public bool isAlive;
    private Player_controller player;
    public float damage;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject bodyPivot;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] UnityEngine.UI.Image healthbar;
    [SerializeField] Canvas canvas;
    [SerializeField] private float health;
    [SerializeField] private float jumpOnDieAmount;

   // private bool forceSwitch;
    void Start()
    {
        player = (Player_controller)FindAnyObjectByType(typeof(Player_controller));
        attackSound.Play();
        isAlive = true;
        rb= GetComponent<Rigidbody>();
        maxHealth = health;
    }

    void Update()
    {
        FallDown(); 
        FollowAndLookPlayer();
       // canvas.transform.LookAt(GameObject.Find("Player").transform);
        healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
        if(!isAlive)
        {
            attackSound.Stop();
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
   public  void Explode()
    {
        if(isAlive) 
        {
            explosionSound.Play();
            health = 0;
            Instantiate(explosionEffect, bodyPivot.transform.position, Quaternion.identity);
            rb.AddForceAtPosition(new Vector3(Random.Range(-8.0f, 8.0f), jumpOnDieAmount, Random.Range(-8.0f, 8.0f)), bodyPivot.transform.position, ForceMode.Impulse);
        }
       
       
    }
    private void FollowAndLookPlayer()
    {
       
        if (isAlive)
        {
            transform.LookAt((GameObject.Find("Player").transform));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }

    }
    public IEnumerator Die()
    {
       
        rb.freezeRotation = false;
        isAlive = false;
      
       
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void GetBodyDamage(float damage)
    {
        health -= damage;
    }

    public void GetHeadDamage(float damage)
    {
        Explode();
    }

    private void FallDown()
    {
        if (rb.velocity.y < -0.1)
        {
            transform.position -= new Vector3(0, 0.01f, 0);
        }
    }

}
