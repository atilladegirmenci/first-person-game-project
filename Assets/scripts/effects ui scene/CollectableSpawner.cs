using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject heal;
    static public CollectableSpawner Instance;
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public bool CheckForPercentage(int percentage)
    {
        int s = Random.Range(1, 101);
        if (s <= percentage) {  return true;  }
        else  return false; 
    }
    public void SpawnHeal(int perentage, Vector3 pos)
    {
        if(CheckForPercentage(perentage))
        {
            Instantiate(heal,new Vector3(pos.x,0.5f,pos.z) ,transform.rotation);
        }
    }
    
}

