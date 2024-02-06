using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float DefBulletLife;
    [SerializeField] private GameObject bulletHole; 
    static public Bullet instance;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private float bulletSpeed;
    [SerializeField] public float bulletDamage;
    private Gun_attributes recoil;

    
    private float recoilX;
    private float recoilY;
    

    private Rigidbody rb;
    
    
    
  

    void Start()
    {
        
        instance = this;
       // enemy = FindAnyObjectByType<Enemy>();
        recoil = FindAnyObjectByType<Gun_attributes>();
        recoilY = recoil.recoilAmountY;
        recoilX = recoil.recoilAmountX;
        
      
        
        rb = gameObject.GetComponent<Rigidbody>();
        BulletTravel();
        StartCoroutine(DestroyBullet(DefBulletLife));
    }

    
    void Update()
    {
       
        
    }
    private void BulletTravel()
    {
        
        float x = Screen.width / 2f + (Random.Range(-1.0f,1.0f) * recoilX);
        float y = Screen.height / 2f + (Random.Range(-1.0f, 1.0f) * recoilY);
        //RaycastHit hit;
        //Vector3 look =Camera.main.transform.TransformDirection(Vector3.forward);
        //Physics.Raycast(Camera.main.transform.position, look, out hit);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x,y,0)) ;
        

         rb.AddForce(ray.direction.normalized * bulletSpeed , ForceMode.Impulse);
       

       
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.contacts[0].otherCollider.transform.gameObject.tag == "enemy head")
        {
           // Instantiate(bloodEffect, collision.contacts[0].point, Quaternion.identity);
            collision.gameObject.GetComponent<Enemy>().GetDamage(bulletDamage *5);

            
            Debug.Log("HEAD SHOT");
        }
        else if (collision.contacts[0].otherCollider.transform.gameObject.tag == "enemy body")
        {
            //Instantiate(bloodEffect, collision.contacts[0].point, Quaternion.identity);
            collision.gameObject.GetComponent<Enemy>().GetDamage(bulletDamage);

            Debug.Log("BODY SHOT");
        }
        if(collision.gameObject.layer  == 10 || collision.gameObject.layer == 6)
        {
            var newEffect =   Instantiate(bulletHole, collision.contacts[0].point +( collision.contacts[0].normal*.05f), Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal));
            newEffect.transform.parent = collision.gameObject.transform;
          
        }
       StartCoroutine(DestroyBullet(0));
        
    }

    private IEnumerator DestroyBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag  == "bullet")
    //    {
    //        Debug.Log("Enemy hit !!!");
    //    }
    //}
}
