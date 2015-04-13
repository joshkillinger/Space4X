using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour
{
	public float AtmoPressure = 0;
	public int AvgTemp;
	public int Rads;
	public float Gravity;

	public int SurfaceMinerals = 0;
	public int SurfaceCrystals = 0;
	public int SurfaceOrganics = 0;

	public int UnminedMinerals = 0;
	public int UnminedCrystals = 0;
	public int UnminedOrganics = 0;

	void Start()
	{
		float atmo = Random.Range(0, 3);
		for (int i = 0; i < atmo; i++)
		{
			AtmoPressure += Random.value * 3.3f;
		}

		AvgTemp = Random.Range(0, 200) + Random.Range(0, 200) + Random.Range(0, 200);

		Rads = Random.Range(0, AvgTemp);

		AvgTemp -= 274;

		Gravity = Random.Range(.033f, 1.5f) + Random.Range(.033f, 1.5f) + Random.Range(.033f, 1.5f);

		UnminedMinerals = Random.Range(0, 10000);
		UnminedCrystals = Random.Range(0, 10000);
		UnminedOrganics = Random.Range(0, 10000);
	}

	public string Info
	{
		get
		{
			string info = "Planet at " + transform.position.x + "," + transform.position.y + ":" + System.Environment.NewLine;
			info += "Atmospheric Pressure: " + AtmoPressure + "A" + System.Environment.NewLine;
			info += "Average Temperature: " + AvgTemp + "C" + System.Environment.NewLine;
			info += "Gravity: " + Gravity + "G" + System.Environment.NewLine;
			info += "Radiation: " + Rads + "mR" + System.Environment.NewLine;
			info += "Minerals (Unmined): " + SurfaceMinerals + " (" + UnminedMinerals + ")" + System.Environment.NewLine;
			info += "Crystals (Unmined): " + SurfaceCrystals + " (" + UnminedCrystals + ")" + System.Environment.NewLine;
			info += "Organics (Unmined): " + SurfaceOrganics + " (" + UnminedOrganics + ")" + System.Environment.NewLine;
			return info;
		}
	}
}
