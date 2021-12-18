using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTowerGhost : MonoBehaviour
{
    [SerializeField]
    GameObject baseTower, turretTower;
    [SerializeField]
    Material towerGhost, towerGhostInvalid;

    //public bool checkPlacementAreas = false;

    
    bool _checkPositionTower = false;
    public bool checkPositionTower { get { return _checkPositionTower; } }

    public void SetMaterialTowerGhost()
    {
        baseTower.GetComponent<Renderer>().material = towerGhost;
        turretTower.GetComponent<Renderer>().material = towerGhost;
    }

    public void SetMaterialTowerGhostInvalid()
    {
        baseTower.GetComponent<Renderer>().material = towerGhostInvalid;
        turretTower.GetComponent<Renderer>().material = towerGhostInvalid;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tower") && other.gameObject != this && !other.isTrigger)
        {
            _checkPositionTower = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tower") && other.gameObject != this && !other.isTrigger)
        {
            _checkPositionTower = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower") && other.gameObject != this && checkPositionTower && !other.isTrigger)
        {
            _checkPositionTower = false;
        }
    }
}
