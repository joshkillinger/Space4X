using UnityEngine;
using System.Collections;

public class GenerateGalaxy : MonoBehaviour
{
	public static int Size = 250;
	public static int Density = 6;
	public static GalaxyType GenerationType = GalaxyType.Random;
	public static bool Generate = false;
	public static bool Load = false;

	public GameObject PlanetPrefab;

	public enum GalaxyType
	{
		Random = 0,
		Spiral = 1,
		Grid = 2
	}

	void Start()
	{
		if (Generate)
		{
			GeneratePlanets();
			Generate = false;
		}
		else if (Load)
		{
			LoadGame();
			Load = false;
		}
		//otherwise do nothing
	}

	void LoadGame()
	{
		throw new System.NotImplementedException("Load game not yet implemented");
	}

	public void GeneratePlanets()
	{
		Debug.Log("Generating planets in " + GenerationType.ToString() + " mode.");
		switch (GenerationType)
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
				Debug.LogError("Oops! Default switch condition reached in GeneratePlanets() with " + GenerationType.ToString());
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
		int planetCount = (Size / Density) * 2;
		//find the max width/height of the galaxy
		int maxRange = Size / 2;

		for (int i = 0; i < planetCount; i++)
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
		int planetCount = Size / Density;
		
		int maxRange = (int)Mathf.Ceil(planetCount);
		maxRange += (maxRange % 2);

		int maxJitter = Density / 2;

		for (int y = -(maxRange / 2); y < (maxRange / 2); y++)
		{
			for (int x = -(maxRange / 2); x < (maxRange / 2); x++)
			{
				//sometimes there is a gap
				if (Random.Range(0, 3) == 0) continue;

				int xpos = (x * Size) + Random.Range(-maxJitter, maxJitter);
				int ypos = (y * Size) + Random.Range(-maxJitter, maxJitter);
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
			if (distance.sqrMagnitude < (Density * Density))
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

	#region Setup
	public void SelectSize(int size)
	{
		Size = size;
	}

	public void SelectDensity(int density)
	{
		Density = density;
	}

	public void SelectType(int type)
	{
		GenerationType = (GenerateGalaxy.GalaxyType)type;
	}

	public void CreateGalaxyBtnClicked()
	{
		Generate = true;
		Application.LoadLevel("MainScene");
	}

	public void CancelBtnClicked()
	{
		//reset to defaults
		Size = 250;
		Density = 6;
		GenerationType = GalaxyType.Random;
		Generate = false;
		Load = false;

		//return to launch screen
		Application.LoadLevel("Launch");
	}
	#endregion
}
