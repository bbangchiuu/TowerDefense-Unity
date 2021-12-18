using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTower : MonoBehaviour
{
    public MyTowerGhost towerGhost;
    public MyTowerLevel[] towerLevels;
    public MyTowerLevel currentTowerLevel;

    public int indexLevel;
    public bool isAtMaxLevel = false;
    public float damageCurrent;
    public float speedAttack;
    public MyNodePlace nodePlaceCurrent;

    /// <summary>
    /// ??????
    /// </summary>
    public IntVector2 gridPosition { get; private set; }

    private void Start()
    {
        indexLevel = 0;
    }

    private void OnMouseDown()
    {
        if (MyGameManager.instance.isBuildingTower)
        {
            return;
        }
        MyGameManager.instance.TowerControllerUI.BuildTower(this, Vector3.zero);
    }

    public InforTower GetInforNextLevel()
    {
        CheckVaildLevel();
        if (isAtMaxLevel)
        {
            return towerLevels[indexLevel].inforTower;
        }
        return towerLevels[indexLevel + 1].inforTower;
    }

    void SetUpCollider()
    {
        Rigidbody rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rigidbody.isKinematic = true;
        BoxCollider boxCollider = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        boxCollider.center = new Vector3(0, 0.75f, 0);
        boxCollider.size = new Vector3(1.8f, 1.5f, 1.8f);
    }

    public void SetUpTower(MyNodePlace myNodePlace)
    {
        CheckVaildLevel();

        Destroy(towerGhost.gameObject);
        nodePlaceCurrent = myNodePlace;
        currentTowerLevel = Instantiate(towerLevels[indexLevel], transform.position, transform.rotation);
        currentTowerLevel.transform.SetParent(this.gameObject.transform);

        damageCurrent = towerLevels[indexLevel].inforTower.damge;
        speedAttack = towerLevels[indexLevel].inforTower.speedAttack;

        SetUpCollider();
    }

    public void UpgradeTower()
    {
        indexLevel++;
        CheckVaildLevel();
        Destroy(currentTowerLevel.gameObject);

        currentTowerLevel = Instantiate(towerLevels[indexLevel], transform.position, transform.rotation);
        currentTowerLevel.transform.SetParent(this.gameObject.transform);

        damageCurrent = currentTowerLevel.inforTower.damge;
        speedAttack = towerLevels[indexLevel].inforTower.speedAttack;
    }

    public void CheckVaildLevel()
    {
        if (indexLevel < 0)
        {
            indexLevel = 0;
        }
        else if (indexLevel >= towerLevels.Length - 1)
        {
            indexLevel = towerLevels.Length - 1;
            isAtMaxLevel = true;
        }
    }
}
