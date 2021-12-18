using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnTowerBtn : MonoBehaviour
{
    public Text buttonText;
    public Image towerIcon;
    public Button buyButton;
    public Image energyIcon;
    public Color energyDefaultColor;
    public Color energyInvalidColor;

    int amountEnegry;
    public MyTower m_Tower;

    bool isBuild = false;
    public bool notEnoughEnergy = false;
    void Start()
    {
        amountEnegry = m_Tower.towerLevels[0].inforTower.enegry;
        buttonText.text = amountEnegry.ToString();
        buttonText.color = Color.black;
    }
    
    void Update()
    {
        if (isBuild)
        {
            ChanggeColorButton();
            return;
        }

        if (MyGameManager.instance.playerManager.currentEnergy < amountEnegry)
        {
            ChanggeColorButton();
            buttonText.color = Color.red;
            notEnoughEnergy = true;
        }
        else
        {
            ResetButton();
            buttonText.color = Color.black;
            notEnoughEnergy = false;
        }
    }

    //Chọn Tower để build
    public void OnClick()
    {
        if (isBuild || MyGameManager.instance.isBuildingTower || notEnoughEnergy)
        {
            return;
        }
        //gửi sang bên manager để build;
        MyGameManager.instance.SetTowerToBuild(m_Tower, this);
        isBuild = true;
        ChanggeColorButton();
    }

    void ChanggeColorButton()
    {
        Color clickColor;
        ColorUtility.TryParseHtmlString("#636363", out clickColor);
        buyButton.GetComponent<Image>().color = clickColor;
    }
    public void ResetButton()
    {
        isBuild = false;
        buyButton.GetComponent<Image>().color = Color.white;
    }
}