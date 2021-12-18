using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public NodeLocation destination;

    public bool isDead = false;

    public InforEnemy currentInfoEnemy;
    public Transform visuals;

    [SerializeField]
    float currentHealth;
    float maxHealth;
    public int currentDamage;
    int currentForward;
    int currentScore;

    //component
    public HealthBarVisual healthBar;
    public NavMeshAgent agent;
    public new GameObject particleSystem;

    private void Awake()
    {
        particleSystem.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(destination != null && !isDead)
        {
            agent.destination = destination.transform.position;
        }
    }

    public void Initialize(int level, NodeLocation nodeSelect)
    {
        destination = nodeSelect;
        maxHealth = currentInfoEnemy.startHealth + currentInfoEnemy.upgradeHealth * level;
        currentHealth = maxHealth;
        currentDamage = currentInfoEnemy.startDamage + currentInfoEnemy.upgradeDamage * level;
        currentForward = currentInfoEnemy.forwardEnergy + currentInfoEnemy.upgradeForward * level;
        currentScore = currentInfoEnemy.startScore + currentInfoEnemy.upgradeScore * level;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Died();
        }

        healthBar.UpdateHealth(currentHealth / maxHealth);
    }

    IEnumerator Dead(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
        yield return 0;
    }

    void Died()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        particleSystem.SetActive(true);
#pragma warning disable CS0618 // Type or member is obsolete
        agent.Stop();
#pragma warning restore CS0618 // Type or member is obsolete
        MyGameManager.instance.playerManager.UpdateEnergy(currentForward);
        MyGameManager.instance.playerManager.UpdateScore(currentScore);
        StartCoroutine(Dead(1f));
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.GetComponent<MyHomeBase>() != null)
        {
            Destroy(gameObject);
            return;
        }

        NodeLocation nodeSelect = other.GetComponent<NodeLocation>();
        if (nodeSelect != null && nodeSelect.GetNextNode() != null)
        {
            destination = nodeSelect.GetNextNode();
        }
    }

    public Vector3 position
    {
        get { return gameObject.transform.position; }
    }

    private void OnParticleCollision(GameObject other)
    {
        MyTower hitByTower = other.GetComponentInParent<MyTower>();
        if(hitByTower != null)
        {
            TakeDamage(hitByTower.damageCurrent);
        }
    }
}
