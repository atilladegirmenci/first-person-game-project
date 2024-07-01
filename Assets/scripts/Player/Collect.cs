using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        ICollectable collectable = other.gameObject.GetComponent<ICollectable>();
        if(collectable != null)
        {
            collectable.Collect();
        }
        
    }
}
