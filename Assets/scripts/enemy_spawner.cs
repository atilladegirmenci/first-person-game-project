using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    private enum enemyTypeEnum { exlposive}
    [SerializeField] private enemyTypeEnum enemyType;
    [SerializeField] private GameObject explosiveEnemy;
    float resetCldwn;
    bool _switch;
    void Start()
    {
        _switch = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChecForEnemyType();
    }

    private void ChecForEnemyType()
    {
        if(enemyType == enemyTypeEnum.exlposive)
        {
            EnemySpawner(explosiveEnemy, 10);
        }
    }

    private void EnemySpawner(GameObject enemytype,float cooldown)
    {
        if(_switch)
        {
            resetCldwn = cooldown;
            _switch = false;
        }
        
        if(resetCldwn <= 0)
        {
            Instantiate(enemytype,new Vector3(Random.Range(-20,21),1.5f,50), transform.rotation);
            resetCldwn = cooldown;
        }
        else
        {
            resetCldwn -= Time.deltaTime;
        }

    }
}
