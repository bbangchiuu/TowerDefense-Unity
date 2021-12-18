using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
    public bool isDrag = true;
    public float height = 0.5f;

    private void OnMouseDown()
    {
        isDrag = !isDrag;
    }

    private void Update()
    {
        //tim ra huong camera den cube

        var cameraToCube = (transform.position - Camera.main.transform.position).normalized;

        RaycastHit ray;
        if(Physics.Raycast(transform.position , -transform.up ,out ray , 100f))
        {
            Debug.DrawRay(transform.position, transform.position - transform.up * 100f);
            height = ray.normal.y;
        }

        if (isDrag)
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

            transform.position = new Vector3(transform.position.x, 
                Mathf.Clamp(transform.position.y, height, 100), 
                transform.position.z);
        }
    }

}
