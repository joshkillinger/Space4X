using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIScript : MonoBehaviour
{
	public Text Name;
	public Text Location;
	public Text Atmosphere;
	public Text Gravity;
	public Text Temperature;
	public Text Radiation;

	private GameObject selectedObject;
	public GameObject SelectedObject
	{
		get
		{
			return selectedObject;
		}
		set
		{
			selectedObject = value;
			ObjectSelected();
		}

	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void ObjectSelected()
	{
		PlanetSelected();
	}

	private void PlanetSelected()
	{
		PlanetScript planet = selectedObject.GetComponent<PlanetScript>();

		Name.text = "Planet Name: " + planet.PlanetName;
		Location.text = "Planet Location: " + planet.transform.position.x + "," + planet.transform.position.y;
		Atmosphere.text = "Atmosphere: " + planet.AtmoPressure + "A";
		Gravity.text = "Gravity: " + planet.Gravity + "G";
		Temperature.text = "Temperature: " + planet.AvgTemp + "C";
		Radiation.text = "Radiation: " + planet.Rads + "mR";
	}
}