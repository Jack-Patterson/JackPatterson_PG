using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private float panSpeed = 60f;
	private float panBorderThickness = 10f;
	private Vector2 panLimit = new Vector2(40,35);

	private float scrollSpeed = 5f;
	
	private float minY = 2f;
	private float maxY = 20f;

	Vector3 pos;

	void Update()
	{
		pos = transform.localPosition;

		if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) && Input.mousePosition.y < Screen.height)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				pos.x += (panSpeed * 2f) * Time.deltaTime;
			}
			else
			{
				pos.x += panSpeed * Time.deltaTime;
			}
		}
		if ((Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) && Input.mousePosition.y > 0)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				pos.x -= (panSpeed * 2f) * Time.deltaTime;
			}
			else
			{
				pos.x -= panSpeed * Time.deltaTime;
			}
		}
		if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) && Input.mousePosition.x < Screen.width)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				pos.z -= (panSpeed * 2f) * Time.deltaTime;
			}
			else
			{
				pos.z -= panSpeed * Time.deltaTime;
			}
		}
		if ((Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) && Input.mousePosition.x > 0)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				pos.z += (panSpeed * 2f) * Time.deltaTime;
			}
			else
			{
				pos.z += panSpeed * Time.deltaTime;
			}
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
		pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

		transform.localPosition = pos;

	}
}
