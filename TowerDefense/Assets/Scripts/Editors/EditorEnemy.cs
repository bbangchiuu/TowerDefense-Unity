using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyController))]
public class EditorEnemy : Editor
{
	EnemyController enemyController;
	void OnEnable()
	{
		enemyController = (EnemyController)target;

		enemyController.healthBar = enemyController.gameObject.GetComponentInChildren<HealthBarVisual>();
		enemyController.agent = enemyController.gameObject.GetComponent<NavMeshAgent>();
	}
}
#endif