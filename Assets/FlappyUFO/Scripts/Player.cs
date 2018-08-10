using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/Player Controller")]
public class Player : MonoBehaviour {

	//Jump Height
	float jumpHeight = 8.0f;

	//Alive Flag
	[HideInInspector]
	public bool isAlive = true;

	//sound effect of reach pillar
	public AudioClip ReachSound = null;

	//define top and bottom of the playable screen
	public Transform top = null;
	public Transform down = null;

	//this is the GameOver Overlay gameObject
	public Transform gameOverUI = null;

	//define GUI Skin
	public GUISkin mySkin = null;

	//this variable contain the game highScore
	[HideInInspector]
	public int highScore = 0;

	//score Container
	[HideInInspector]
	public int score = 0;

	//player Default Texture
	public Texture PlayerDefaultTexture = null;

	//die flag
	bool DieFlag = false;

	//Death animation texture Array
	public Texture[] DeathAnim = null;

	void Start()
	{
		//load saved highscore from saved values
		highScore = PlayerPrefs.GetInt("HighScore");

		//if player texture is defined on component so use it.
		if (PlayerDefaultTexture != null)
			GetComponent<Renderer>().material.mainTexture = PlayerDefaultTexture;
	}

	//GUI SYSTEM
	void OnGUI()
	{
		//if we have skin setted on player then show the Score
		if (mySkin != null)
		{
			//set the skin
			GUI.skin = mySkin;
			//draw label for showing score
			GUI.Label(new Rect(Screen.width / 2 - 10, 20, Screen.width, 100), score.ToString());

			//when player is dead show the score on the gameover UI
			if (!isAlive)
			{
				//if the score position transform is available on GameOver UI then show the score
				if (gameOverUI.GetComponent<gameOver>().scoreText != null)
				{
					//get the 3D Text and then show the score there
					gameOverUI.GetComponent<gameOver>().scoreText.GetComponent<TextMesh>().text = score.ToString();
				}
				//if the score position transform is available on GameOver UI then show the score
				if (gameOverUI.GetComponent<gameOver>().scoreText != null)
				{
					//get the 3D Text and then show the score there
					gameOverUI.GetComponent<gameOver>().HighScoreText.GetComponent<TextMesh>().text = highScore.ToString();
				}
			}
		}
	}

	//this function handle the UFO Jump when player tap the screen
	void Tap()
	{
		//when player tap or hit mouse Left
		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			//change the position of the UFO in Y axis by using the JumpHeight Variable
			transform.GetComponent<Rigidbody>().velocity = new Vector2(0,jumpHeight);
		}
	}

	IEnumerator DeathAnimation()
	{
		for (int i = 0; i < DeathAnim.Length; i++)
		{
			GetComponent<Renderer>().material.mainTexture = DeathAnim[i];
			yield return new WaitForSeconds(0.1f);
		}
		yield return null;
	}

	void Die()
	{
		//check for die Flag to do this expressions once
		if (!DieFlag)
		{
			//play death animation if animation images are set
			if (DeathAnim != null)
				StartCoroutine("DeathAnimation");

			//check for score if it is more than saved highscore we save the new score
			//as highscore
			if (score > highScore)
			{
				//set the gold medal because player reach new highscore
				if (gameOverUI.GetComponent<gameOver>() != null)
				{
					//then set The Gold Texture On it if available
					if (gameOverUI.GetComponent<gameOver>().GoldMedal != null)
					{
						gameOverUI.GetComponent<gameOver>().Medal.GetComponent<Renderer>().material.mainTexture = gameOverUI.GetComponent<gameOver>().GoldMedal;
					}
					//now it's time to activate Medal Gameobject to show the new medal
					gameOverUI.GetComponent<gameOver>().Medal.gameObject.SetActive(true);

					//the new score reached so we enable the NEW indicator newar the best score too
					gameOverUI.GetComponent<gameOver>().NewHighScore.gameObject.SetActive(true);
				}

				PlayerPrefs.SetInt("HighScore", score);
				highScore = PlayerPrefs.GetInt("HighScore");
			}

			//when the player score is more than highscore / 2 and less than the highscore 
			//player get the silver medal
			if (score > (highScore / 2) && score < highScore)
			{
				//set the gold medal because player reach new highscore
				if (gameOverUI.GetComponent<gameOver>() != null)
				{
					//then set The Gold Texture On it if available
					if (gameOverUI.GetComponent<gameOver>().SilverMedal != null)
					{
						gameOverUI.GetComponent<gameOver>().Medal.GetComponent<Renderer>().material.mainTexture = gameOverUI.GetComponent<gameOver>().SilverMedal;
					}
					//now it's time to activate Medal Gameobject to show the new medal
					gameOverUI.GetComponent<gameOver>().Medal.gameObject.SetActive(true);
				}
			}

			//when the player score is more than highscore / 3 and less than the highscore 
			//and less than highscore / 2
			//player get the bronze medal
			if (score > (highScore / 3) && score < highScore && score < (highScore /2))
			{
				//set the gold medal because player reach new highscore
				if (gameOverUI.GetComponent<gameOver>() != null)
				{
					//then set The Gold Texture On it if available
					if (gameOverUI.GetComponent<gameOver>().BronzeMedal != null)
					{
						gameOverUI.GetComponent<gameOver>().Medal.GetComponent<Renderer>().material.mainTexture = gameOverUI.GetComponent<gameOver>().BronzeMedal;
					}
					//now it's time to activate Medal Gameobject to show the new medal
					gameOverUI.GetComponent<gameOver>().Medal.gameObject.SetActive(true);
				}
			}

			//turn the isKinematic on to hold player anywhere this hit the collider
			transform.GetComponent<Rigidbody>().isKinematic = true;

			//and set the gravityscale to 0
			transform.GetComponent<Rigidbody>().useGravity = false;

			//when the palyer is died the Game Over Screen will appears
			//and when this appeared and you tap the game will restarted 
			gameOverUI.gameObject.SetActive(true);

			Debug.Log("DIED");

			//enable the die flag to do this once because we check dieFlag at the first line of this function
			DieFlag = true;
		}
	}

	//check when something hit the ufo
	void OnTriggerEnter(Collider col)
	{
		//when player hit something is not the score counter trigger
		//so it's going to dead.
		if (col.GetComponent<ScoreCounter>() == null)
		{
			//we will die
			isAlive = false;
		}
	}

	// Update is called once per frame
	void Update () {
		//Die check for player
		if (!isAlive)
			Die ();

		//call tap function each Fixed frame
		//when player is under top indicator (gameObject)
		if (transform.position.y <= top.position.y + 3 && isAlive)
			Tap();

		//when player is lower down gameObject it means the ship hit the ground
		//so it's died
		//check if player hit the ground by DOWN gameObject defined on component
		if (transform.position.y < down.position.y & isAlive) 
		{
			isAlive = false;
		}
	}
}
