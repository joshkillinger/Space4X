using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public float ScrollSpeed;
	public float PanSpeed;
	public float MaxZoom;
	public float MinZoom;

	public GameObject UIPanel;

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

			if (transform.position.z > MinZoom)
			{
				Vector3 clampedPosition = transform.position;
				clampedPosition.z = MinZoom;
				transform.position = clampedPosition;
			}
			if (transform.position.z < MaxZoom)
			{
				Vector3 clampedPosition = transform.position;
				clampedPosition.z = MaxZoom;
				transform.position = clampedPosition;
			}
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
				GameUIScript uiScript = UIPanel.GetComponent<GameUIScript>();
				uiScript.SelectedObject = clickHit.transform.gameObject;
			}
		}
		else
		{
			Debug.Log("Traveled " + panDistance);
		}
	}
}