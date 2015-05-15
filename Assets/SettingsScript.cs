using UnityEngine;
using System.Collections;

public class SettingsScript : MonoBehaviour
{
	public int GalaxySize = 100;
	public int GalaxyDensity = 3;
	public GenerateGalaxy.GalaxyType GalaxyType = GenerateGalaxy.GalaxyType.Random;

	public void SelectSize(int size)
	{
		GalaxySize = size;
	}

	public void SelectDensity(int density)
	{
		GalaxyDensity = density;
	}

	public void SelectType(int type)
	{
		GalaxyType = (GenerateGalaxy.GalaxyType)type;
	}
}
