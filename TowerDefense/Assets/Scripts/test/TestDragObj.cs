using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDragObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector3 postion = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(postion);

        transform.position = new Vector3(worldPostion.x, worldPostion.y, worldPostion.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
