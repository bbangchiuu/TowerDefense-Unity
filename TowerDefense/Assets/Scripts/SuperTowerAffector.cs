using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTowerAffector : FiringAttack
{
    [SerializeField]
    new ParticleSystem particleSystem;
    [SerializeField]
    FollowEnemy followEnemy;
    [SerializeField]
    Transform[] projectilePoint;

    float m_FireTimer = 0;
    float shootInterval = 0.1f;
    int indexProjectilePoint = 0;

    float m_FireTurn = 0;
    float shootIntervalTurn = 2;
    bool turnStarted;

    int numberBulletsturn = 20;
    int numberBulletsFired = 0;

    [SerializeField]
    GameObject ballistic;
    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        indexProjectilePoint = 0;
        MyTower myTower = GetComponentInParent<MyTower>();
        shootIntervalTurn = myTower.speedAttack;
        turnStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FireTurn <= 0)
        {
            turnStarted = true;
            m_FireTurn = shootIntervalTurn;
        }

        if (!turnStarted)
        {
            m_FireTurn -= Time.deltaTime;
            return;
        }

        EnemyController currentEnemy = followEnemy.currentEnemy;
        if (currentEnemy != null)
        {
            if (currentEnemy.isDead)
            {
                m_FireTimer = 0;
                indexProjectilePoint = 0;
                turnStarted = true;
                m_FireTurn = 0;
                return;
            }

            m_FireTimer -= Time.deltaTime;
            if (m_FireTimer <= 0)
            {
                if (indexProjectilePoint >= projectilePoint.Length)
                {
                    indexProjectilePoint = 0;
                }

                PlayParticles(particleSystem, projectilePoint[indexProjectilePoint].position, currentEnemy.visuals.position);

                Fire(currentEnemy);
                numberBulletsFired++;
                m_FireTimer = shootInterval;
                indexProjectilePoint++;
                audioSource.Play();
                if(numberBulletsFired >= numberBulletsturn)
                {
                    turnStarted = false;
                    numberBulletsFired = 0;
                    m_FireTurn = shootIntervalTurn;
                }
            }
        }
        else
        {
            turnStarted = false;
            numberBulletsFired = 0;
            m_FireTurn = shootIntervalTurn;
        }
    }

    public override void Fire(EnemyController currentEnemy)
    {
        float damageCurrent = GetComponentInParent<MyTower>().damageCurrent;
        MyBallistic __ballistic = Instantiate(ballistic, projectilePoint[indexProjectilePoint].position, projectilePoint[indexProjectilePoint].rotation).GetComponent<MyBallistic>();
        __ballistic.Initialize(currentEnemy, damageCurrent);
    }
}
