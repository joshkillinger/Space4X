using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public float ScrollSpeed;
	public float PanSpeed;

	public GameObject SelectedObject;

	private float panDistance = 0;

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		MouseZoom();

		//when first pressed
		if (Input.GetMouseButtonDown(0))
		{
			//reset pan distance
			panDistance = 0;
		}

		if (Input.GetMouseButton(0))
		{
			MousePan();
		}

		if (Input.GetMouseButtonUp(0))
		{
			CheckForClick();
		}
	}

	private void MouseZoom()
	{
		float mouseZ = Input.GetAxis("Mouse ScrollWheel");
		if (mouseZ != 0)
		{
			transform.Translate(Vector3.forward * mouseZ * ScrollSpeed);
		}
	}

	private void MousePan()
	{
		float dx = Input.GetAxis("Mouse X");
		float dy = Input.GetAxis("Mouse Y");

		//track total distance moved
		panDistance += Mathf.Abs(dx);
		panDistance += Mathf.Abs(dy);

		//calculate pan distance
		float zoomFactor = transform.position.z * -1;
		zoomFactor *= PanSpeed;

		//pan the camera
		transform.Translate(Vector3.left * dx * zoomFactor);
		transform.Translate(Vector3.down * dy * zoomFactor);
	}

	private void CheckForClick()
	{
		if (panDistance < .05)
			{
			//Debug.Log("No travel");
			Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit clickHit;
			if (Physics.Raycast(clickRay, out clickHit, Mathf.Infinity))
			{
				SelectedObject = clickHit.transform.gameObject;
				Debug.Log(SelectedObject.name);
			}
		}
		else
		{
			Debug.Log("Traveled " + panDistance);
		}
	}
}