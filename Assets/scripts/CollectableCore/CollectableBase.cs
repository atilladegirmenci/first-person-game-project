using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBase : MonoBehaviour
{
    private Rigidbody rb;
    public enum CollectableTypes
    {
        Null,
        PistolAmmo,
        ShotgunAmmo,
        RifleAmmo,
        Heal
    }
    public CollectableTypes type;
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        ThrowOnSpawn();

    }
    protected virtual void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
    private void ThrowOnSpawn()
    {
        rb.AddRelativeForce(new Vector3(Random.Range(-100f, 100f), 400f, Random.Range(-100f, 100f)));
    }
    public virtual void Collected()
    {

    }
   
}
