using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBallistic : MonoBehaviour
{
    new Rigidbody rigidbody;

    float ballisticVelocity = 15f;
    float turn = 10f;
    float damage = 10f;

    EnemyController target;

    [SerializeField]
    Transform positionExplosion;
    [SerializeField]
    ParticleSystem collisionParticles;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        rigidbody.velocity = transform.forward * ballisticVelocity;

        var ballisticRotation = Quaternion.LookRotation(target.visuals.position - transform.position);
        rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, ballisticRotation, turn));
    }
    
    public void Initialize(EnemyController enemy, float dmg)
    {
        target = enemy;
        damage = dmg;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target.gameObject)
        {
            Instantiate(collisionParticles, positionExplosion.position, positionExplosion.rotation);
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
