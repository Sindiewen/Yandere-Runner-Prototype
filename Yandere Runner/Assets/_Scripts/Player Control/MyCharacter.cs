// MyCharacter.cs - A simple example showing how to get input from Rewired.Player

using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class MyCharacter : MonoBehaviour {

	public int playerId = 0; // The Rewired player id of this character

	public float moveSpeed = 3.0f;
	public float bulletSpeed = 15.0f;
	public GameObject bulletPrefab;

	[Header("Jump")]
	public float JumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private Player player; // The Rewired Player
	private CharacterController cc;
	private Vector3 moveVector;
	private bool fire;


	void Awake() {
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);

		// Get the character controller
		cc = GetComponent<CharacterController>();
	}

	void Update () {
		GetInput();
		ProcessInput();
	}

	private void GetInput() {
		// Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
		// whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

		moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
		//moveVector.y = player.GetAxis("Jump");

		fire = player.GetButtonDown("Fire");
	}

	private void ProcessInput() {
		// Process movement
		if(moveVector.x != 0.0f)
		{
			cc.Move(moveVector * moveSpeed * Time.deltaTime);
		}

		if (Input.GetButtonDown("Jump"))
		{
			moveVector.y = JumpSpeed;
		}


		// Process fire
		if(fire) {
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
		}


		// Processess Gravity
		moveVector.y -= gravity * Time.deltaTime;
	}

}