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
			//generate random x,y coordinates centered on 0,0
			int x = Random.Range(-maxRange, maxRange);
			int y = Random.Range(-maxRange, maxRange);

			//check for collision with another planet
			Ray collisionRay = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
			RaycastHit collideHit;
			if (Physics.Raycast(collisionRay, out collideHit, Mathf.Infinity))
			{
				i--;
				Debug.Log("Hit an existing planet, try again.");
			}
			else
			{
				//TODO: Check for min distance to other planets

				CreatePlanet(new Vector3(x, y, 0));
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
	private void CreatePlanet(Vector3 position)
	{
		GameObject planet = Instantiate(PlanetPrefab, position, Quaternion.identity) as GameObject;
	}
}
