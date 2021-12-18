using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;

    public int currentEnergy;
    public int currentHealth;
    public int currentScore;

    [SerializeField]
    Text textHealth, textEnegry, textScore;
    private void Start()
    {
        currentEnergy = playerData.maxEnergy;
        currentHealth = playerData.maxHealth;
        currentScore = 0;

        textEnegry.text = currentEnergy.ToString();
        textHealth.text = currentHealth.ToString();
        textScore.text = currentScore.ToString();
    }

    public void UpdateEnergy(int energy)
    {
        currentEnergy += energy;
        textEnegry.text = currentEnergy.ToString();
    }

    public void UpdateHealth(int healt)
    {
        currentHealth += healt;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            MyGameManager.instance.GameOver(currentScore);
        }
        textHealth.text = currentHealth.ToString();
    }

    public void UpdateScore(int score)
    {
        currentScore += score;
        textScore.text = currentScore.ToString();
    }
}
