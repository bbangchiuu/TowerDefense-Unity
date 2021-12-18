using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject);
    }

    private void OnMouseDown()
    {
        Debug.Log("dang cahy");
    }
}
