using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyEnemy/Create")]
public class InforEnemy : ScriptableObject
{
    public string nameEnemy;

    public float startHealth;
    public float upgradeHealth;

    public int forwardEnergy;
    public int upgradeForward;

    public int startDamage;
    public int upgradeDamage;

    public int startScore;
    public int upgradeScore;
}
