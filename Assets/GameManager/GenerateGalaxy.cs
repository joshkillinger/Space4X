using UnityEngine;
using System.Collections;

public class GenerateGalaxy : MonoBehaviour
{
	public int Spread;
	public int PlanetCount;
	public int MinDistance;

	public GameObject PlanetPrefab;

	public void GeneratePlanets()
	{
		int axis = (int)Mathf.Ceil(Mathf.Sqrt(PlanetCount));
		if ((axis % 2) == 1)
		{
			axis++;
		}

		int maxJitter = (Spread / 2) - MinDistance;

		for (int y = -(axis / 2); y < (axis / 2); y++)
		{
			for (int x = -(axis / 2); x < (axis / 2); x++)
			{
				if (Random.Range(0, 4) == 0) continue;

				int xpos = (x * Spread) + Random.Range(-maxJitter, maxJitter);
				int ypos = (y * Spread) + Random.Range(-maxJitter, maxJitter);
				CreatePlanet(new Vector3(xpos, ypos, 0));
			}
		}
	}

	private void CreatePlanet(Vector3 position)
	{
		GameObject planet = Instantiate(PlanetPrefab, position, Quaternion.identity) as GameObject;
	}
}
