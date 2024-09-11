using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject heal;
    [SerializeField] private GameObject pistolBullets;
    [SerializeField] private GameObject rifleBullets;
    [SerializeField] private GameObject shotgunShells;
    [SerializeField] private int bulletSpawnPercentage;
    [SerializeField] private int healSpawnPercentage;   
   
    
    static public CollectableSpawner Instance;
    void Start()
    {
        Instance = this;
    }


    public void SpawnCollectableOnDie(Vector3 pos)
    {
        SpawnBullet(pos);
        SpawnHeal(pos);
    }
    private bool CheckForPercentage(int percentage)
    {
        int s = Random.Range(1, 101);
        if (s <= percentage) {  return true;  }
        else  return false; 
    }
    private void SpawnHeal( Vector3 pos)
    {
        if(CheckForPercentage(healSpawnPercentage))
        {
            Instantiate(heal,new Vector3(pos.x,0.5f,pos.z) ,transform.rotation);
        }
    }
    private void SpawnBullet( Vector3 pos)
    {
       if(CheckForPercentage(bulletSpawnPercentage))
       {
            int n = Random.Range(1, 4);
            switch(n)
            {
                case 1: Instantiate(pistolBullets, new Vector3(pos.x, 0.5f, pos.z), transform.rotation);
                    break;

                case 2: Instantiate(rifleBullets, new Vector3(pos.x, 0.5f, pos.z), transform.rotation);
                     break;

                case 3: Instantiate(shotgunShells, new Vector3(pos.x, 0.5f, pos.z), transform.rotation);
                    break;

                default: 
                    break;
            }
                

            
                
       }

    }
    
}

