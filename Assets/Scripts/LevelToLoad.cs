using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad : MonoBehaviour
{
	public string loadLevel;
	public string exitName; // string/name
	private PlayerController thePlayer;

	void Start ()
	{
		LoadLevel ();
		thePlayer = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}
	void LateUpdate ()
	{
		//LoadLevel ();
	}
	void LoadLevel ()
	{
		if (thePlayer.levelRayUpActive || thePlayer.levelRayDownActive || thePlayer.levelRayLeftActive || thePlayer.levelRayRightActive)
		{
			Debug.Log ("!");
			SceneManager.LoadScene (loadLevel);
			thePlayer.startPoint = exitName;
		}
	}
}