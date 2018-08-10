using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/Start Button")]
public class StartButton : MonoBehaviour {

	//the variable for level name
	public string LevelName = "";

	//tap flag
	bool tapped = false;


	//this is flag for activating this tap to start
	bool canTap = false;

	// Use this for initialization
	void Start () 
	{
		//we wait 0.5 second before start tapping
		StartCoroutine("wait");

	}

	IEnumerator wait()
	{
		// we wait for 0.5 seconds
		yield return new WaitForSeconds(0.5f);
		canTap = true;
		yield return null;
	}

	// Update is called once per frame
	void Update () 
	{
		//when player tap th screen or press mouse Left this will load the desired level
		if (Input.GetKey (KeyCode.Mouse0) && !tapped && canTap) {
			tapped = true;
			Application.LoadLevel(LevelName);
		}

		//when press back button exit application
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

	}
}
