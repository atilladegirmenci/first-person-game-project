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
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ChecForEnemyType();
    }

    private void ChecForEnemyType()
    {
        

        if(enemyType1 == enemyTypeEnum.exlposive)
        {
            if (resetCldwn1 <= 0)
            {
                EnemySpawner(explosiveEnemy);
                resetCldwn1 = 8;
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
                resetCldwn2 = 7;
            }
            else
            {
                resetCldwn2 -= Time.deltaTime;
            }
        }
    }

    private void EnemySpawner(GameObject enemytype)
    {
        Instantiate(enemytype, new Vector3(Random.Range(-20, 21), 1.5f, 50), transform.rotation);


    }

}
