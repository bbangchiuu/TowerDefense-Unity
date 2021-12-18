using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiringAttack : MonoBehaviour
{
	public abstract void Fire(EnemyController currentEnemy);

	public void PlayParticles(ParticleSystem particleSystemToPlay, Vector3 origin, Vector3 lookPosition)
	{
		if (particleSystemToPlay == null)
		{
			return;
		}

		particleSystemToPlay.transform.position = origin;
		particleSystemToPlay.transform.LookAt(lookPosition);
		particleSystemToPlay.Play();
	}
}
