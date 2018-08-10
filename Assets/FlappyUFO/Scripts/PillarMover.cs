using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/Pillar Mover (Auto)")]
public class PillarMover : MonoBehaviour {

	Vector3 tempPosition = Vector3.zero;
	Vector3 myPosition = Vector3.zero;

	//get value from Generator
	public Transform screenLEFT;

	//definition of player
	Player player = null;

	//Speed of column
	[HideInInspector]
	public float speed = 0.1f;

	void Start()
	{
		//find the player
		player = (Player) FindObjectOfType(typeof(Player));
		//when init call MoveMe
		StartCoroutine("MoveMe");
	}

	IEnumerator MoveMe()
	{
		//get transform current position
		myPosition = transform.position;

		//set temporary position;
		tempPosition = myPosition - new Vector3(speed,0,0);

		//move the transform to the left
		transform.position = tempPosition;

		//return for 1 frame
		yield return null;
		//when reach the screen side destroy this gameobject
		if (screenLEFT.position.x > transform.position.x)
			Destroy(gameObject);

		//if player isAlive move the object
		if (player.GetComponent<Player>().isAlive)
		StartCoroutine("MoveMe");
	}
}
