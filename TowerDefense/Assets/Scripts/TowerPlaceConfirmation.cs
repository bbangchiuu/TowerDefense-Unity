using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlaceConfirmation : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    private void Start()
    {
        panel.SetActive(false);
    }

    public void ActiveUI()
    {
        panel.SetActive(true);
    }

    //Xác nhận build
    public void ConfirmTowerPlace()
    {
        MyGameManager.instance.ConfirmTowerPlace();
        panel.SetActive(false);
    }

    //Hủy build
    public void CloseSetUpTower()
    {
        MyGameManager.instance.CloseSetUpTower();
        panel.SetActive(false);
    }
}
