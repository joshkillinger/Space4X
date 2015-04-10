using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour
{
	public float AtmoPressure = 0;
	public int AvgTemp;
	public int Rads;
	public float Gravity;

	public int SurfaceAdamantine = 0;
	public int SurfaceRadite = 0;
	public int SurfaceGermanium = 0;

	public int UnminedAdamantine = 0;
	public int UnminedRadite = 0;
	public int UnminedGermanium = 0;

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

		UnminedAdamantine = Random.Range(0, 10000);
		UnminedRadite = Random.Range(0, 10000);
		UnminedGermanium = Random.Range(0, 10000);
	}

}
