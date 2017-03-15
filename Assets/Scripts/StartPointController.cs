using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointController : MonoBehaviour
{
	private PlayerController thePlayer;
	private CameraController theCamera;

	public Vector2 startDirection;
	public string pointName; // string/name

	void Start ()
	{
		thePlayer = FindObjectOfType<PlayerController> ();
		theCamera = FindObjectOfType<CameraController> ();

		if (thePlayer.startPoint == pointName)
		{
			thePlayer.transform.position = this.gameObject.transform.position;
			thePlayer.lastMove = startDirection;
			theCamera.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, theCamera.transform.position.z);
		}
	}
}