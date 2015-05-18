using UnityEngine;
using System.Collections;
using System.IO;

public class PlanetScript : MonoBehaviour
{
	public float AtmoPressure = 0;
	public float Gravity;
	public int AvgTemp;
	public int Rads;

	public int SurfaceMinerals = 0;
	public int SurfaceCrystals = 0;
	public int SurfaceOrganics = 0;

	public int UnminedMinerals = 0;
	public int UnminedCrystals = 0;
	public int UnminedOrganics = 0;

	public string PlanetName = "";

	private static ArrayList planetNames = new ArrayList();

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

		CreateName();
	}


	public string Info
	{
		get
		{
			string info = "Planet " + PlanetName + " at " + transform.position.x + "," + transform.position.y + ":" + System.Environment.NewLine;
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

	#region Naming
	private void CreateName()
	{
		while (true)
		{
			string name = "";

			name += GetNamePrefix();
			name += GetNameFromFile("Planets");
			name += GetNameSuffix();

			if (!planetNames.Contains(name))
			{
				PlanetName = name;
				planetNames.Add(name);
				break;
			}
		}
	}

	private string GetNamePrefix()
	{
		string name = "";
		int prefix = Random.Range(0, 100);
		if (prefix < 10)
		{
			name += "New ";
			if (prefix == 0)
			{
				Debug.Log("New New! hehe");
				name += "New ";
			}
		}

		if (Random.Range(0, 10) == 0)
		{
			name += GetNameFromFile("Prefixes") + " ";
		}

		return name;
	}

	private string GetNameSuffix()
	{
		string name = "";
		int suffix = Random.Range(0, 20);
		if (suffix == 0)
		{
			name += " Major";
		}
		else if (suffix == 1)
		{
			name += " Minor";
		}
		else if (suffix == 2)
		{
			name += " Australis";
		}

		if (Random.Range(0, 10) == 0)
		{
			name += " " + GetNameFromFile("Suffixes");
		}
		return name;
	}

	/// <summary>
	/// Get a random line from a file
	/// </summary>
	/// <param name="filename">GenData\[filename].txt</param>
	/// <returns>Random line from the specified file</returns>
	private static string GetNameFromFile(string filename)
	{
		filename = "Assets\\GameManager\\GenData\\" + filename + ".txt";
		StreamReader reader = new StreamReader(filename);
		string rawData = reader.ReadToEnd();
		string[] splitchars = { "\r\n" };
		string[] splitData = rawData.Split(splitchars, System.StringSplitOptions.RemoveEmptyEntries);

		string name = splitData[Random.Range(0, splitData.Length)];
		return name;
	}
	#endregion
}
