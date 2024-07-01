using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofSpawnPoint : MonoBehaviour
{
    public bool isColliding;
    static public RoofSpawnPoint instance;
    void Start()
    {
      
        instance = this;    
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        isColliding=true;
    }
    private void OnTriggerExit(Collider other)
    {
        isColliding=false;
    }
}
