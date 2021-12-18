using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHomeBase : MonoBehaviour
{
    [SerializeField]
    ParticleSystem attackPfx, chargeEffect;
    [SerializeField]
    AudioClip zoneEnter, baseAttack;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            int dmgAttack = other.GetComponent<EnemyController>().currentDamage;
            chargeEffect.Play();
            audioSource.clip = zoneEnter;
            audioSource.Play();
            StartCoroutine(AttackPlayerHome(audioSource.clip.length, dmgAttack));
            Destroy(other.gameObject);
        }
    }

    IEnumerator AttackPlayerHome(float _timer, int dmgAttack)
    {
        yield return new WaitForSeconds(_timer);
        attackPfx.Play();
        audioSource.clip = baseAttack;
        audioSource.Play();
        MyGameManager.instance.playerManager.UpdateHealth(-dmgAttack);
    }
}
