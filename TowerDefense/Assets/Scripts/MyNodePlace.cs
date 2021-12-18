using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyNodePlace : MonoBehaviour
{
    [SerializeField]
    Material notHover, hoverColor;
    public bool isBuiled = false;
    new Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

    }

    //Chọn vị trí để build
    private void OnMouseDown()
    {
        if (MyGameManager.instance.IsPointerOverUIObject() || MyGameManager.instance.isBuildingTower || isBuiled)
        {
            return;
        }

        if (MyGameManager.instance.currentTower != null)
        {
            isBuiled = true;
            renderer.material = hoverColor;
            //Chuyển sang bên manager để thiết lập model tower
            MyGameManager.instance.SelectNodePlace(this);
        }
    }

    public void ResetNodePlace()
    {
        isBuiled = false;
        renderer.material = notHover;
    }

    private void OnMouseEnter()
    {
        if (MyGameManager.instance.currentTower == null || MyGameManager.instance.isBuildingTower || isBuiled)
        {
            return;
        }
        renderer.material = hoverColor;
    }

    private void OnMouseExit()
    {
        if (isBuiled)
        {
            return;
        }
        renderer.material = notHover;
    }   
}
