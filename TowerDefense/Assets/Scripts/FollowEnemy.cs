using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField]
    Transform turret;

	public List<EnemyController> listEnemies = new List<EnemyController>();
	public EnemyController currentEnemy;

	public Collider attachedCollider;

	float m_SearchTimer = 0.0f;
	float searchRate = 0;
	float m_WaitTimer = 4.0f;

	float m_CurrentRotationSpeed;
	float idleRotationSpeed = 39f;
	float m_XRotationCorrectionTime = 0f;
	float idleCorrectionTime = 2.0f;
	float idleWaitTime = 2.0f;

	bool onlyYTurretRotation;
	Vector2 turretXRotationRange = new Vector2(0, 359);

	public float radiusRange;
	//float m_FireTimer = 0;
	//public float shootInterval = 0.2f;

	// Start is called before the first frame update
	void Start()
    {
        if(attachedCollider == null)
        {
			attachedCollider = GetComponent<Collider>();
        }
	}

    // Update is called once per frame
    void Update()
    {
		if (currentEnemy != null && currentEnemy.isDead)
		{
			ResetTargetEnemy();
		}

		if (m_SearchTimer <= 0.0f && currentEnemy == null && listEnemies.Count > 0)
		{
			currentEnemy = GetNearestEnemy();
			if (currentEnemy != null)
			{
				m_SearchTimer = searchRate;
			}
		}

		AimTurret();
	}

	EnemyController GetNearestEnemy()
	{
		int length = listEnemies.Count;

		if (length == 0)
		{
			return null;
		}

		EnemyController nearest = null;
		float distance = float.MaxValue;
		for (int i = length - 1; i >= 0; i--)
		{
			EnemyController targetable = listEnemies[i];
			if (targetable == null || targetable.isDead)
			{
				listEnemies.RemoveAt(i);
				continue;
			}
			float currentDistance = Vector3.Distance(transform.position, targetable.position);
			if (currentDistance < distance)
			{
				distance = currentDistance;
				nearest = targetable;
			}
		}

		return nearest;
	}

	void AimTurret()
	{
		if (currentEnemy == null) // do idle rotation
		{
			if (m_WaitTimer > 0)
			{
				m_WaitTimer -= Time.deltaTime;
				if (m_WaitTimer <= 0)
				{
					m_CurrentRotationSpeed = (Random.value * 2 - 1) * idleRotationSpeed;
				}
			}
			else
			{
				Vector3 euler = turret.rotation.eulerAngles;
				euler.x = Mathf.Lerp(Wrap180(euler.x), 0, m_XRotationCorrectionTime);
				m_XRotationCorrectionTime = Mathf.Clamp01((m_XRotationCorrectionTime + Time.deltaTime) / idleCorrectionTime);
				euler.y += m_CurrentRotationSpeed * Time.deltaTime;

				turret.eulerAngles = euler;
			}
		}
		else
		{
			m_WaitTimer = idleWaitTime;

            Vector3 targetPosition = currentEnemy.visuals.position;
            if (onlyYTurretRotation)
            {
                targetPosition.y = turret.position.y;
            }
            Vector3 direction = targetPosition - turret.position;
            Quaternion look = Quaternion.LookRotation(direction, Vector3.up);
            Vector3 lookEuler = look.eulerAngles;

            // We need to convert the rotation to a -180/180 wrap so that we can clamp the angle with a min/max
            float x = Wrap180(lookEuler.x);
            lookEuler.x = Mathf.Clamp(x, turretXRotationRange.x, turretXRotationRange.y);
            look.eulerAngles = lookEuler;
            turret.rotation = look;
            //turret.transform.LookAt(currentEnemy.transform.position);
		}
	}
	
	private void OnTriggerEnter(Collider other)
    {
		EnemyController enemy = other.GetComponent<EnemyController>();
		if (enemy != null)
        {	
			listEnemies.Add(enemy);
		}
    }

    private void OnTriggerExit(Collider other)
    {
		EnemyController enemy = other.GetComponent<EnemyController>();
		if (enemy != null)
		{
			if (currentEnemy == enemy)
            {
				ResetTargetEnemy();
			}
			listEnemies.Remove(enemy);
		}
    }

	void ResetTargetEnemy()
    {
		currentEnemy = null;
    }
	static float Wrap180(float angle)
	{
		angle %= 360;
		if (angle < -180)
		{
			angle += 360;
		}
		else if (angle > 180)
		{
			angle -= 360;
		}
		return angle;
	}
}
