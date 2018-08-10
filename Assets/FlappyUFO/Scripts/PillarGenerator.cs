using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/Pillar Generator")]
public class PillarGenerator : MonoBehaviour {

	//get pillars entity
	public Transform[] Pillars = null;

	//define the side of screen via an gameObject
	public Transform ScreenSide = null;

	public float pillarSpeed = 0.1f;

	//add new gameobject
	public Transform pillar;

	//this define spacce between each pillar
	public float spaceBetween = 7.0f;

	//variable for setting boundry for Y min and Max of Pillars
	float yMinPillars = -2.0f;
	float yMaxPillars = 5.0f;
	
	//definition of player
	Player player = null;

	//we want to count how much column generated so define a variable
	[HideInInspector]
	public int colNumber = 0;

	GameObject tempPillar = null;

	//Pillar Generator
	IEnumerator generator()
	{
		//if we have new pillar
		if (pillar != null)
		{
			//check for distance from this transform to the prevoius pillar for next pillar Generation
			if (transform.position.x - pillar.position.x > spaceBetween)
			{
				//generate new pillar
				topDown(Random.Range(0, Pillars.Length));
			}
		}
		yield return null;
		StartCoroutine("generator");
	}

	//this function generates the Top-Down Position of each column
	void topDown(int pillarNo)
	{
		//count for new col
		colNumber++;

		//pillar = new Transform();
		//respawn a new pillar from desired pillars on component
		pillar = (Transform) Instantiate(Pillars[pillarNo], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Pillars[pillarNo].rotation);
		//Name it with count
		pillar.name = "Pillar " + colNumber.ToString();
		//add Mover Component on the new GameObject
		pillar.gameObject.AddComponent<PillarMover>();
		//set the Screenside On Pillar Mover
		pillar.GetComponent<PillarMover>().screenLEFT = ScreenSide;
		//set the speed on pillarMover
		pillar.GetComponent<PillarMover>().speed = pillarSpeed;

		//generate random Y position and set on generated new object
		//the Ymin and Max set the Y position of pillars refer to the position of the starting pillar Y
		Vector3 tempPosition = pillar.position;
		pillar.position = new Vector3(tempPosition.x, tempPosition.y + Random.Range(yMinPillars, yMaxPillars), tempPosition.z);

	}

	// Use this for initialization
	void Start () 
	{
		//generate the first pillar now
		topDown(Random.Range(0, Pillars.Length));

		player = (Player) FindObjectOfType(typeof(Player));

		//call generator coroutine for generate more pillars
		StartCoroutine("generator");
	}
}
