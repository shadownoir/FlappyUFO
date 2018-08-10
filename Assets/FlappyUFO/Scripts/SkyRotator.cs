using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/SkyRotator")]
public class SkyRotator : MonoBehaviour {

	//define an array for SkyImages
	public Texture[] SkyImages = null;

	//define a material for getting the main bg material for sky image
	public Material SkyMaterial = null;

	//new variable to hold a random number
	int randomCount = 0;

	// Use this for initialization
	void Start () 
	{
		//when the skymaterial and skyimages are present then we start to randomly
		//change the BG of game
		if (SkyMaterial != null && SkyImages != null) 
		{
			//count a random value from 0 to total images added from component by user
			randomCount = Random.Range (0, SkyImages.Length);

			//chenge the skyMaterial to the desired random image.
			SkyMaterial.mainTexture = SkyImages[randomCount];
		}
	}
}
