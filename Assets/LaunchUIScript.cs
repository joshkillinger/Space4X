using UnityEngine;
using System.Collections;

public class LaunchUIScript : MonoBehaviour
{
	public void NewGame()
	{
		Application.LoadLevel("NewGame");
	}

	public void Exit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
