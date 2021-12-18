using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Reference: class TargetterEditor
/// </summary>
#if UNITY_EDITOR
[CustomEditor(typeof(FollowEnemy))]
public class EditorFollowEnemy : Editor
{
    public enum TargetterCollider
	{
        Sphere = 0,
        Capsule = 1
    }

	FollowEnemy m_followEnemy;

	TargetterCollider m_ColliderConfiguration;
	Collider m_AttachedCollider;

	/// The radius of the collider
	float m_ColliderRadius;
	/// The height of a capsule collider
	float m_ExtraVerticalRange;

	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Targetter Collider Configuration", EditorStyles.boldLabel);

		m_ColliderConfiguration = (TargetterCollider)EditorGUILayout.EnumPopup("Targetter Collider", m_ColliderConfiguration);
		AttachCollider();

		//m_ColliderRadius = EditorGUILayout.FloatField("Radius", m_followEnemy.radiusRange);

		//if (m_ColliderConfiguration == TargetterCollider.Capsule)
		//{
		//	m_ExtraVerticalRange = EditorGUILayout.FloatField("Vertical Range", m_followEnemy.radiusRange);
		//}

		EditorUtility.SetDirty(m_followEnemy);
		EditorUtility.SetDirty(m_AttachedCollider);

		serializedObject.ApplyModifiedProperties();
    }

	/// For attaching and hiding the correct collider
	void AttachCollider()
	{
		switch (m_ColliderConfiguration)
		{
			case TargetterCollider.Sphere:
				if (m_AttachedCollider is SphereCollider)
				{
					GetValues();
					return;
				}
				if (m_AttachedCollider != null)
				{
					DestroyImmediate(m_AttachedCollider, true);
				}
				m_AttachedCollider = m_followEnemy.gameObject.AddComponent<SphereCollider>();
				break;
			case TargetterCollider.Capsule:
				if (m_AttachedCollider is CapsuleCollider)
				{
					GetValues();
					return;
				}
				if (m_AttachedCollider != null)
				{
					DestroyImmediate(m_AttachedCollider, true);
				}
				m_AttachedCollider = m_followEnemy.gameObject.AddComponent<CapsuleCollider>();
				break;
		}
		SetValues();
		//m_AttachedCollider.hideFlags = HideFlags.HideInInspector;
	}

	/// Obtains the information from the collider
	void GetValues()
	{
		switch (m_ColliderConfiguration)
		{
			case TargetterCollider.Sphere:
				var sphere = (SphereCollider)m_AttachedCollider;
				m_ColliderRadius = sphere.radius;
				break;
			case TargetterCollider.Capsule:
				var capsule = (CapsuleCollider)m_AttachedCollider;
				m_ColliderRadius = capsule.radius;
				m_ExtraVerticalRange = capsule.height - m_ColliderRadius * 2;
				break;
		}
	}

	/// Assigns the values to the collider
	void SetValues()
	{
		switch (m_ColliderConfiguration)
		{
			case TargetterCollider.Sphere:
				var sphere = (SphereCollider)m_AttachedCollider;
				sphere.radius = m_ColliderRadius;
				break;
			case TargetterCollider.Capsule:
				var capsule = (CapsuleCollider)m_AttachedCollider;
				capsule.radius = m_ColliderRadius;
				capsule.height = m_ExtraVerticalRange + m_ColliderRadius * 2;
				break;
		}
	}

	/// Caches the collider and hides it
	/// and configures all the necessary information from it
	void OnEnable()
	{
		m_followEnemy = (FollowEnemy)target;

		m_ColliderRadius = m_followEnemy.radiusRange;
		m_ExtraVerticalRange = m_followEnemy.radiusRange;

		if (m_AttachedCollider == null)
		{
			m_AttachedCollider = m_followEnemy.GetComponent<Collider>();
			if (m_AttachedCollider == null)
			{
				switch (m_ColliderConfiguration)
				{
					case TargetterCollider.Sphere:
						m_AttachedCollider = m_followEnemy.gameObject.AddComponent<SphereCollider>();
						break;
					case TargetterCollider.Capsule:
						m_AttachedCollider = m_followEnemy.gameObject.AddComponent<CapsuleCollider>();
						break;
				}
			}
		}
		if (m_AttachedCollider is SphereCollider)
		{
			m_ColliderConfiguration = TargetterCollider.Sphere;
		}
		else if (m_AttachedCollider is CapsuleCollider)
		{
			m_ColliderConfiguration = TargetterCollider.Capsule;
		}

		m_followEnemy.attachedCollider = m_followEnemy.gameObject.GetComponent<Collider>();
		SetValues();
		GetValues();
		m_AttachedCollider.isTrigger = true;
		//m_AttachedCollider.hideFlags = HideFlags.HideInInspector;
	}
}
#endif