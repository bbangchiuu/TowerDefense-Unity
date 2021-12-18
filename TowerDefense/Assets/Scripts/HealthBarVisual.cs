using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarVisual : MonoBehaviour
{
    [SerializeField]
    GameObject healthBar;
	Camera cam;

    void Update()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }

        if (cam == null)
        {
            Debug.Log("cam null");
            return;
        }

        transform.LookAt(cam.transform);
        //transform.Rotate(Vector3.up * 180);
    }

    public void UpdateHealth(float normalizedHealth)
	{
		Vector3 scale = Vector3.one;
		if (healthBar != null)
		{
			scale.x = normalizedHealth;
			healthBar.transform.localScale = scale;
		}
	}
}
