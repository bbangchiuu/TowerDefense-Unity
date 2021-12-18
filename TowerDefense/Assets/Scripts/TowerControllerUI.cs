using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerControllerUI : MonoBehaviour
{
    public GameObject panel;
    public GameObject hideBlock;

    //Description tower
    public Text towerName;
    public Text towerDamageCurrent;
    public Text towerDescription;

    //update tower
    public Text energyText;
    public Text upgradeDescription;
    public Button upgradeButton;

    //sell
    public Text selEnergyText;
    public Button sellConfirmButton;

    MyTower currentTower;

    private void Start()
    {
        HidePanel();
    }

    public void BuildTower(MyTower tower, Vector3 direction)
    {
        InforTower inforTower = tower.currentTowerLevel.inforTower;
        towerName.text = inforTower.nameTower;
        towerDamageCurrent.text = inforTower.damge.ToString();
        towerDescription.text = inforTower.description;
        selEnergyText.text = inforTower.selEnegry.ToString();

        if (tower.isAtMaxLevel)
        {
            energyText.text = inforTower.enegry.ToString();
            upgradeDescription.text = inforTower.upgradeDescription.ToString();
            upgradeButton.interactable = false;
        }
        else
        {
            InforTower inofrNextLevel = tower.GetInforNextLevel();
            energyText.text = inofrNextLevel.enegry.ToString();
            upgradeDescription.text = "Sát thương: +" + (inofrNextLevel.damge - inforTower.damge) + ", " + inforTower.upgradeDescription.ToString();

            if (MyGameManager.instance.playerManager.currentEnergy < inofrNextLevel.enegry)
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }
        }

        currentTower = tower;

        StartCoroutine(WaiTimeActive());
        panel.SetActive(true);

    }

    private IEnumerator WaiTimeActive()
    {
        yield return new WaitForSeconds(0.5f);
        hideBlock.SetActive(true);
    }

    public void HidePanel()
    {
        currentTower = null;
        panel.SetActive(false);
        hideBlock.SetActive(false);
        sellConfirmButton.gameObject.SetActive(false);
    }

    public void UpgradeButtonOnclick()
    {
        currentTower.UpgradeTower();
        MyGameManager.instance.playerManager.UpdateEnergy(-currentTower.currentTowerLevel.inforTower.enegry);
        HidePanel();
    }

    public void SelButtonOnclick()
    {
        sellConfirmButton.gameObject.SetActive(true);
    }

    public void SelButtonConfirmOnclick()
    {
        currentTower.nodePlaceCurrent.ResetNodePlace();
        MyGameManager.instance.playerManager.UpdateEnergy(currentTower.currentTowerLevel.inforTower.selEnegry);
        Destroy(currentTower.gameObject);
        HidePanel();
    }
}
