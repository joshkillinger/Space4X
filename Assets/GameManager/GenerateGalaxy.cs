using UnityEngine;
using System.Collections;

public class GenerateGalaxy : MonoBehaviour
{
	public int Spread;
	public int PlanetCount;
	public int MinDistance;

	public GameObject PlanetPrefab;

	public enum GalaxyType
	{
		Grid,
		Spiral,
		Random
	}

	public void GeneratePlanets(GalaxyType type)
	{
		switch (type)
		{
			case GalaxyType.Grid:
				GenerateGrid();
				break;

			case GalaxyType.Random:
				GenerateRandom();
				break;

			case GalaxyType.Spiral:
				GenerateSpiral();
				break;

			default:
				//we shouldn't ever get here, but generate a Grid anyway
				Debug.LogError("Oops! Default switch condition reached in GeneratePlanets() with " + type.ToString());
				GenerateGrid();
				break;
		}
	}

	private void GenerateSpiral()
	{
		throw new System.NotImplementedException();
	}

	private void GenerateRandom()
	{
		//find the max width/height of the galaxy
		int maxRange = (int)Mathf.Ceil(Mathf.Sqrt(PlanetCount));
		maxRange += (maxRange % 2);

		//scale up the galaxy
		maxRange *= Spread;
		maxRange /= 2;

		for (int i = 0; i < PlanetCount; i++)
		{
			bool failed = true;

			for (int tries = 0; tries < 100; tries ++)
			{
				//generate random x,y coordinates centered on 0,0
				int x = Random.Range(-maxRange, maxRange);
				int y = Random.Range(-maxRange, maxRange);

				//validate position
				Vector3 position = new Vector3(x, y, 0);
				if (CheckForOtherPlanet(position))
				{
					//A good position, create the planet
					CreatePlanet(position);
					failed = false;
					break;
				}
			}

			if (failed)
			{
				Debug.Log("Couldn't find valid location in 100 tries, ending galaxy generation after creating " + (i - 1) + " planets.");
				break;
			}
		}
	}

	private void GenerateGrid()
	{
		//find the max width/height of the galaxy
		int maxRange = (int)Mathf.Ceil(Mathf.Sqrt(PlanetCount));
		maxRange += (maxRange % 2);

		//shift the planets slightly so they aren't perfectly aligned
		int maxJitter = (Spread / 2) - MinDistance;

		for (int y = -(maxRange / 2); y < (maxRange / 2); y++)
		{
			for (int x = -(maxRange / 2); x < (maxRange / 2); x++)
			{
				//sometimes there is a gap
				if (Random.Range(0, 3) == 0) continue;

				int xpos = (x * Spread) + Random.Range(-maxJitter, maxJitter);
				int ypos = (y * Spread) + Random.Range(-maxJitter, maxJitter);
				CreatePlanet(new Vector3(xpos, ypos, 0));
			}
		}
	}

	/// <summary>
	/// Verifies that a new planet is not within MinDistance of an existing planet
	/// </summary>
	/// <param name="position">Position of new planet</param>
	/// <returns>True if valid, false if too close to another planet</returns>
	private bool CheckForOtherPlanet(Vector3 position)
	{
		//check for collision with another planet
		//Ray collisionRay = Camera.main.ScreenPointToRay(position);
		//RaycastHit collideHit;
		//if (Physics.Raycast(collisionRay, out collideHit, Mathf.Infinity))
		//{
		//    Debug.Log("Hit an existing planet, try again.");
		//    return false;
		//}
		//else
		//{
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Planet"))
		{
			Vector3 distance = p.transform.position - position;
			if (distance.sqrMagnitude < (MinDistance * MinDistance))
			{
				Debug.Log("Too close to an existing planet, try again.");
				return false;
			}
		}
		//}

		return true;
	}

	/// <summary>
	/// Instantiates a new planet
	/// </summary>
	/// <param name="position">Placement of planet in the coordinate system</param>
	private void CreatePlanet(Vector3 position)
	{
		GameObject planet = Instantiate(PlanetPrefab, position, Quaternion.identity) as GameObject;
	}
}
