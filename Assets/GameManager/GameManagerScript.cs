using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		Generate();
	}

	public void Generate()
	{
		GenerateGalaxy gen = gameObject.GetComponent<GenerateGalaxy>();
		gen.GeneratePlanets(GenerateGalaxy.GalaxyType.Random);
	}

	public void Save()
	{
	}

	public void Load()
	{
	}

	public void EndTurn()
	{
	}
}