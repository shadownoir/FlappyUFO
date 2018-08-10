using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/GameOver UI")]
public class gameOver : MonoBehaviour {

	//this is for definition of New Indicator Of highscore
	public Transform NewHighScore = null;

	//this is for Medal Transform
	public Transform Medal = null;

	//this is the Medals Textures Textures
	public Texture GoldMedal = null;
	public Texture SilverMedal = null;
	public Texture BronzeMedal = null;

	//this definitions set the 3D texts of score and highscore from GameOver Component
	public Transform scoreText = null;
	public Transform HighScoreText = null;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
