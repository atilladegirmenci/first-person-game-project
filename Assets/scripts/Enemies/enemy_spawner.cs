using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    private enum enemyTypeEnum { exlposive, fast, shooter}
    [SerializeField] private enemyTypeEnum enemyType1, enemyType2, enemyType3;
    private enemyTypeEnum enemyType;
    [SerializeField] private GameObject explosiveEnemy;
    [SerializeField] private GameObject fastEnenmy;
    [SerializeField] private GameObject shooterEnemy;
    [SerializeField] private GameObject grndspwnp1, grndspwnp2, grndspwnp3 ,roofspwnp1, roofspwnp2, roofspwnp3;
    private GameObject[] roofSpawnPoints;
    void Start()
    {
        roofSpawnPoints = new GameObject[3];
        roofSpawnPoints[0] = roofspwnp1;
        roofSpawnPoints[1] = roofspwnp2;
        roofSpawnPoints[2] = roofspwnp3;
        
       StartCoroutine(SpawnerCooldown());
    }

    private Vector3 RandomSpawnPointGround()
    {
        int rndm = UnityEngine.Random.Range(1, 4);
        switch(rndm)
        {
            case 1: return grndspwnp1.transform.position;
            case 2: return grndspwnp2.transform.position;
            case 3: return grndspwnp3.transform.position;
            default: return new Vector3(0,0,0);
        }
    }
    private GameObject RandomSpawnpointRoof()
    {
        int rndm = UnityEngine.Random.Range(1, 4);
        switch(rndm)
        {
            case 1: return roofspwnp1;
            case 2: return roofspwnp2;
            case 3: return roofspwnp3;
            default: return null;
        }
    }

    private enemyTypeEnum  RandomEnumValue()
    {
        var v = Enum.GetValues(typeof(enemyTypeEnum));
        return (enemyTypeEnum)v.GetValue(UnityEngine.Random.Range(0,v.Length));
    }

    private void SetEnemyType()
    {
        enemyType = RandomEnumValue();
        CheckEnemyType();
        Debug.Log(enemyType.ToString());
    }
    private void CheckEnemyType()
    {

        if (enemyType == enemyTypeEnum.exlposive)
        {
            EnemySpawner(explosiveEnemy, RandomSpawnPointGround());
        }
        else if(enemyType == enemyTypeEnum.fast)
        {
            EnemySpawner(fastEnenmy, RandomSpawnPointGround());
        }
        else if(enemyType == enemyTypeEnum.shooter)
        {
            GameObject spwnP = RandomSpawnpointRoof();
            if(IsColliding(spwnP) == true )
            {
                
                if(IsAllPosOccupied() == false)
                {
                    SetEnemyType();
                }
            }
            else
            {
                EnemySpawner(shooterEnemy, spwnP.transform.position);
            }
        }
    }
    private IEnumerator SpawnerCooldown()
    {
        float cooldownTime = UnityEngine.Random.Range(3f, 8f);
        
        yield return new WaitForSeconds(cooldownTime);
       
        SetEnemyType();
    }

    private void EnemySpawner(GameObject enemytype,Vector3 spawnPos)
    {
        Instantiate(enemytype, spawnPos, transform.rotation);
        StartCoroutine(SpawnerCooldown());
    }

    private bool IsAllPosOccupied()
    {
        bool s = false;
        for(int i = 0; i<roofSpawnPoints.Length-1;i++)
        {
            if(roofSpawnPoints[i].GetComponent<RoofSpawnPoint>().isColliding == false && s==false )
            { 
                CheckEnemyType();
                s = true;
               
            }
            else
            {
                continue;
            }
            
        }
        return s;
    }
    private bool IsColliding(GameObject spawnP)
    {
        if(spawnP.GetComponent<RoofSpawnPoint>().isColliding == true)
        {
            return true;
        }
        else return false;
    }

}
