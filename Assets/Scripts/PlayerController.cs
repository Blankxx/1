using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Vector3 pos;
	private float speed = 2f;
	private Animator anim;
	private bool isMoving;
	public Vector2 lastMove;

	public Transform rayUpStart, rayUpEnd;
	public Transform rayDownStart, rayDownEnd;
	public Transform rayLeftStart, rayLeftEnd;
	public Transform rayRightStart, rayRightEnd;

	public bool upBlocked;
	public bool downBlocked;
	public bool leftBlocked;
	public bool rightBlocked;

	public Transform levelRayUpStart, levelRayUpEnd;
	public Transform levelRayDownStart, levelRayDownEnd;
	public Transform levelRayLeftStart, levelRayLeftEnd;
	public Transform levelRayRightStart, levelRayRightEnd;

	public bool levelRayUpActive;
	public bool levelRayDownActive;
	public bool levelRayLeftActive;
	public bool levelRayRightActive;

	private static bool playerinLevel;

	public string startPoint; // string

	void Start ()
	{
		pos = transform.position;
		anim = GetComponent<Animator> ();

		if (!playerinLevel)
		{
			playerinLevel = true;
			DontDestroyOnLoad (transform.gameObject); // when player loads into new scene, gameObject not destroyed.
		}
		else
		{
			Destroy (gameObject);
		}
	}
	void Update ()
	{
		if (levelRayUpActive || levelRayDownActive || levelRayLeftActive || levelRayRightActive)
		{
			//pos = transform.position;
			//pos = new Vector3 (0,0,0);
			pos = transform.position;
			// pos = startPoint or exitCoordinates;
		}
	}
	void LateUpdate ()
	{
		CollisionRaycasting ();
		LevelRaycasting ();
		Move ();
	}
	void Move ()
	{
		if (Input.GetAxisRaw ("Vertical") > 0 && transform.position == pos && !upBlocked || Input.GetAxisRaw ("Vertical") < 0 && transform.position == pos && !downBlocked)
		{
			isMoving = true;
			pos += new Vector3 (0f, Input.GetAxisRaw ("Vertical"), 0f);
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
		}
		if (Input.GetAxisRaw ("Horizontal") < 0 && transform.position == pos && !leftBlocked || Input.GetAxisRaw ("Horizontal") > 0 && transform.position == pos && !rightBlocked)
		{
			isMoving = true;
			pos += new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, 0f);
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
		}
		transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
		if (transform.position == Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed))
		{
			isMoving = false;
		}
		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
		anim.SetBool ("IsMoving", isMoving);
	}
	void CollisionRaycasting ()
	{
		Debug.DrawLine (rayUpStart.position, rayUpEnd.position, Color.red);
		Debug.DrawLine (rayDownStart.position, rayDownEnd.position, Color.red);
		Debug.DrawLine (rayLeftStart.position, rayLeftEnd.position, Color.red);
		Debug.DrawLine (rayRightStart.position, rayRightEnd.position, Color.red);

		StartCoroutine (Delay());
		upBlocked = Physics2D.Linecast (rayUpStart.position, rayUpEnd.position, 1 << LayerMask.NameToLayer ("Obstacle"));
		downBlocked = Physics2D.Linecast (rayDownStart.position, rayDownEnd.position, 1 << LayerMask.NameToLayer ("Obstacle"));
		leftBlocked = Physics2D.Linecast (rayLeftStart.position, rayLeftEnd.position, 1 << LayerMask.NameToLayer ("Obstacle"));
		rightBlocked = Physics2D.Linecast (rayRightStart.position, rayRightEnd.position, 1 << LayerMask.NameToLayer ("Obstacle"));
	}
	void LevelRaycasting ()
	{
		Debug.DrawLine (levelRayUpStart.position, levelRayUpEnd.position, Color.blue);
		Debug.DrawLine (levelRayDownStart.position, levelRayDownEnd.position, Color.blue);
		Debug.DrawLine (levelRayLeftStart.position, levelRayLeftEnd.position, Color.blue);
		Debug.DrawLine (levelRayRightStart.position, levelRayRightEnd.position, Color.blue);

		levelRayUpActive = Physics2D.Linecast (levelRayUpStart.position, levelRayUpEnd.position, 1 << LayerMask.NameToLayer ("LevelZone"));
		levelRayDownActive = Physics2D.Linecast (levelRayDownStart.position, levelRayDownEnd.position, 1 << LayerMask.NameToLayer ("LevelZone"));
		levelRayLeftActive = Physics2D.Linecast (levelRayLeftStart.position, levelRayLeftEnd.position, 1 << LayerMask.NameToLayer ("LevelZone"));
		levelRayRightActive = Physics2D.Linecast (levelRayRightStart.position, levelRayRightEnd.position, 1 << LayerMask.NameToLayer ("LevelZone"));
	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds (1);
	}
}