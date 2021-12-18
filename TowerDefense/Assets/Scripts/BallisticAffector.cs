using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticAffector : FiringAttack
{
    [SerializeField]
    new ParticleSystem particleSystem;
    [SerializeField]
    FollowEnemy followEnemy;
    [SerializeField]
    Transform[] projectilePoint;

    float m_FireTimer = 0;
    float shootInterval;
    int indexProjectilePoint = 0;

    [SerializeField]
    GameObject ballistic;
    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        indexProjectilePoint = 0;
        MyTower myTower = GetComponentInParent<MyTower>();
        shootInterval = myTower.speedAttack;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyController currentEnemy = followEnemy.currentEnemy;
        if(currentEnemy != null)
        {            
            m_FireTimer -= Time.deltaTime;
            if (m_FireTimer <= 0)
            {
                if(indexProjectilePoint >= projectilePoint.Length)
                {
                    indexProjectilePoint = 0;
                }

                PlayParticles(particleSystem, projectilePoint[indexProjectilePoint].position, currentEnemy.visuals.position);

                Fire(currentEnemy);
                m_FireTimer = shootInterval;
                indexProjectilePoint++;
                audioSource.Play();
            }
        }
    }

    public override void Fire(EnemyController currentEnemy)
    {
        float damageCurrent = GetComponentInParent<MyTower>().damageCurrent;
        MyBallistic __ballistic = Instantiate(ballistic, projectilePoint[indexProjectilePoint].position, projectilePoint[indexProjectilePoint].rotation).GetComponent<MyBallistic>();
        __ballistic.Initialize(currentEnemy, damageCurrent);
    }
}
