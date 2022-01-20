using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float panSpeed = 30f;
	public float panBorderThickness = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;

	// Update is called once per frame
	void Update()
	{


		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				transform.Translate(Vector3.back * (panSpeed * 2f) * Time.deltaTime, Space.World);
			}
			else
			{
				transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
			}
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				transform.Translate(Vector3.forward * (panSpeed * 2f) * Time.deltaTime, Space.World);
			}
			else
			{
				transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
			}
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				transform.Translate(Vector3.left * (panSpeed * 2f) * Time.deltaTime, Space.World);
			}
			else
			{
				transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
			}
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				transform.Translate(Vector3.right * (panSpeed * 2f) * Time.deltaTime, Space.World);
			}
			else
			{
				transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
			}
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		transform.position = pos;

	}
}
