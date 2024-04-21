using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    private enum enemyTypeEnum { exlposive, fast}
    [SerializeField] private enemyTypeEnum enemyType1, enemyType2;
    [SerializeField] private GameObject explosiveEnemy;
    [SerializeField]  private GameObject fastEnenmy;
    float resetCldwn1 =3;
    float resetCldwn2 = 3;
    [SerializeField] private GameObject spwnp1, spwnp2, spwnp3;     
    void Start()
    {
       
    }

    void Update()
    {
        ChecForEnemyType();
    }
    private Vector3 RandomSpawnPoint()
    {
        int rndm = Random.Range(1, 4);
        switch(rndm)
        {
            case 1: return spwnp1.transform.position;
            case 2: return spwnp2.transform.position;
            case 3: return spwnp3.transform.position;
            default: return new Vector3(0,0,0);
        }
    }

    private void ChecForEnemyType()
    {
        

        if(enemyType1 == enemyTypeEnum.exlposive)
        {
            if (resetCldwn1 <= 0)
            {
                EnemySpawner(explosiveEnemy);
                resetCldwn1 = 6;
            }
            else
            {
                resetCldwn1 -= Time.deltaTime;
            }
        }

        if(enemyType2 == enemyTypeEnum.fast)
        {

            if (resetCldwn2 <= 0)
            {
                EnemySpawner(fastEnenmy);
                resetCldwn2 = 8;
            }
            else
            {
                resetCldwn2 -= Time.deltaTime;
            }
        }
    }

    private void EnemySpawner(GameObject enemytype)
    {
        Instantiate(enemytype, RandomSpawnPoint(), transform.rotation);


    }

}
