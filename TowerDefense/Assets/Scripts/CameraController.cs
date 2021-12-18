using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    float scrollSpeed = 5f;
    float minY = 10f;
    float maxY = 25;

	float minPosZ = -24;
	float maxPosZ = 20;
	float maxPosX = 42;
	float minPosX = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.touchCount > 0)
        {
			if(Input.touchCount == 2)
            {
				Touch touch0 = Input.GetTouch(0);
				Touch touch1 = Input.GetTouch(1);

				Vector2 touch0PrePos = touch0.position - touch0.deltaPosition;
				Vector2 touch1PrePos = touch1.position - touch1.deltaPosition;
				Debug.Log(touch0PrePos + " - " + touch1PrePos);
				float preMagnitude = (touch0PrePos - touch1PrePos).magnitude;
				float currentMagnitude = (touch0.position - touch1.position).magnitude;

				float different = currentMagnitude - preMagnitude;
				Vector3 zoomCam = transform.position;

				zoomCam.y -= different * Time.deltaTime;
				zoomCam.y = Mathf.Clamp(zoomCam.y, minY, maxY);

				transform.position = zoomCam;

				return;
			}			
			Vector2 inputAxis = Input.GetTouch(0).deltaPosition;
			Vector3 moveCam = new Vector3(inputAxis.x, 0, inputAxis.y);
			transform.Translate(-moveCam * Time.deltaTime, Space.World);
		}
		
		if (Input.GetKey("w"))
		{
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s"))
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d"))
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a"))
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		transform.position = pos;

		if (transform.position.z < minPosZ)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, minPosZ);
		}
		if (transform.position.z > maxPosZ)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, maxPosZ);
		}
		if (transform.position.x < minPosX)
		{
			transform.position = new Vector3(minPosX, transform.position.y, transform.position.z);
		}
		if (transform.position.x > maxPosX)
		{
			transform.position = new Vector3(maxPosX, transform.position.y, transform.position.z);
		}
	}
}
