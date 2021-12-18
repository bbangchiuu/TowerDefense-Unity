using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyTower/Create")]
public class InforTower : ScriptableObject
{
    public string nameTower;
   
    public string description;
    public string upgradeDescription;

    public int enegry;
    public int selEnegry;

    public float speedAttack;
    public float damge;
}
