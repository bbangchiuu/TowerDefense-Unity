using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    Transform nodeEnd, nodeStart;

    [SerializeField]
    GameObject[] enemiesCreep;
    [SerializeField]
    GameObject enemyBoss;

    [SerializeField]
    GameObject[] enemiesSuperCreep;
    [SerializeField]
    GameObject enemySuperBoss;

    public bool isEnemyStart;

    public float turnTimeCreep = 5f;
    public float turnTimeBoss = 10f;
    public float turnTime;

    public float spawnEnemyTurn;

    public float spawnEnemyTime;
    public float resetSpawnTime = 2f;
    [SerializeField]
    int currentQuantityEnemy;
    [SerializeField]
    int totalEnemyTurn = 10;

    [SerializeField]
    int indexEnemy = 0;
    [SerializeField]
    int totalEnemyList = -1;

    [SerializeField]
    bool isCreep, isBoss;

    [SerializeField]
    bool creepCommon, creepSuper;
    [SerializeField]
    bool bossCommon, bossSuper;

    int levelCreep = 0;
    int levelSuperCreep = 0;
    int levelBoss = 0;
    int levelSuperBoss = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyTurn = 0;

        turnTime = turnTimeCreep;
        isCreep = true;
        isBoss = false;

        creepCommon = true;
        creepSuper = false;

        bossCommon = true;
        bossSuper = false;

        currentQuantityEnemy = 0;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemyStart)
        {
            spawnEnemyTurn -= Time.deltaTime;
        }

        if (spawnEnemyTurn <= 0)
        {
            isEnemyStart = true;
            spawnEnemyTurn = turnTime;
            currentQuantityEnemy = 0;
            return;
        }

        if (isEnemyStart && isCreep)
        {
            if (creepCommon)
            {
                if(totalEnemyList == -1)
                {
                    totalEnemyList = enemiesCreep.Length;
                }

                if(currentQuantityEnemy >= totalEnemyTurn)
                {
                    indexEnemy++;
                    isEnemyStart = false;
                    turnTime = turnTimeCreep;
                }

                if (indexEnemy >= totalEnemyList)
                {
                    isCreep = false;
                    creepCommon = false;
                    creepSuper = true;
                    isBoss = true;

                    totalEnemyList = -1;
                    isEnemyStart = false;

                    indexEnemy = 0;
                    turnTime = turnTimeBoss;
                    levelCreep++;
                }

                SpawnEnemyCommon();
            }
            else if (creepSuper)
            {
                if (totalEnemyList == -1)
                {
                    totalEnemyList = enemiesSuperCreep.Length;
                }

                if (currentQuantityEnemy >= totalEnemyTurn)
                {
                    indexEnemy++;
                    isEnemyStart = false;
                    turnTime = turnTimeCreep;
                }

                if (indexEnemy >= totalEnemyList)
                {
                    isCreep = false;
                    creepCommon = true;
                    creepSuper = false;
                    isBoss = true;

                    indexEnemy = 0;
                    totalEnemyList = -1;
                    isEnemyStart = false;

                    turnTime = turnTimeBoss;
                    levelSuperCreep++;
                }
                SpawnEnemySuper();
            }
        }
        else if(isEnemyStart && isBoss)
        {
            if (bossCommon)
            {
                SpawnEnemyBossCommon();
                levelBoss++;
            }
            else if (bossSuper)
            {
                SpawnEnemyBossSuper();
                levelSuperBoss++;
            }

            isCreep = true;
            isBoss = false;
            bossCommon = !bossCommon;
            bossSuper = !bossSuper;

            totalEnemyList = -1;
            isEnemyStart = false;

            turnTime = turnTimeBoss;
        }
    }
    
    void SpawnEnemyCommon()
    {
        spawnEnemyTime += Time.deltaTime;
        if (spawnEnemyTime >= resetSpawnTime)
        {
            EnemyController enemy = Instantiate(enemiesCreep[indexEnemy], nodeStart.position, nodeStart.rotation).GetComponent<EnemyController>();
            enemy.Initialize(levelCreep, nodeEnd);
            spawnEnemyTime = 0;
            currentQuantityEnemy++;
        }
    }

    void SpawnEnemySuper()
    {
        spawnEnemyTime += Time.deltaTime;
        if (spawnEnemyTime >= resetSpawnTime)
        {
            EnemyController enemy = Instantiate(enemiesSuperCreep[indexEnemy], nodeStart.position, nodeStart.rotation).GetComponent<EnemyController>();
            enemy.Initialize(levelSuperCreep, nodeEnd);
            spawnEnemyTime = 0;
            currentQuantityEnemy++;
        }
    }

    void SpawnEnemyBossCommon()
    {
        EnemyController enemy = Instantiate(enemyBoss, nodeStart.position, nodeStart.rotation).GetComponent<EnemyController>();
        enemy.Initialize(levelBoss, nodeEnd);
    }

    void SpawnEnemyBossSuper()
    {
        EnemyController enemy = Instantiate(enemySuperBoss, nodeStart.position, nodeStart.rotation).GetComponent<EnemyController>();
        enemy.Initialize(levelSuperBoss, nodeEnd);
    }
}
