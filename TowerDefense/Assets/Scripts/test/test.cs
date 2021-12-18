using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public GameObject cube;
    GameObject current;
    public Button img;
    Camera cam;

    Vector3 GetMousePositionOnWorld()
    {
        Vector3 postion = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(postion);

        return worldPostion;
    }

    public Vector3 worldPosition;
    public void OnClick()
    {
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        current = Instantiate(cube, worldPosition, Quaternion.identity);

    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePostFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePostNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePostNear, worldMousePostFar - worldMousePostNear, out hit);

        return hit;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
