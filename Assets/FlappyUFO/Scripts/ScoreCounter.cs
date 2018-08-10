using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/Score Counter")]
public class ScoreCounter : MonoBehaviour {

	//definition of player
	Player player = null;

	//flag for add score once
	bool scoreFlag = true;

	// Use this for initialization
	void Start () {
		//get the player component from Scene
		player = (Player) FindObjectOfType(typeof(Player));
	}

	//when player enter the score Trigger
	void OnTriggerEnter(Collider col)
	{
		//if it is player and able to earn score once
		if(player.name == col.name && scoreFlag)
		{
			//add 1 more score to player Score
			player.score++;

			//play a sound for reach pillar
			if (player.ReachSound != null)
			{
				//play SFX once
				player.GetComponent<AudioSource>().PlayOneShot(player.ReachSound);
			}

			//when player get score of reach this pillar we set the score Flag to false
			//to earn score once
			scoreFlag = false;
		}
	}
}
