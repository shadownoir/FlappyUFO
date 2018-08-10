using UnityEngine;
using System.Collections;

[AddComponentMenu ("Kiavash2k/Flappy Ufo/Animated Texture")]
public class animatedTexture : MonoBehaviour 
{
	//Animation rateVector
	Vector2 uvAnimSideAndSpeed = new Vector2(0.8f, 0.0f);

	//define uvOffset
	Vector2 offset = Vector2.zero;

	//definition of player
	Player player = null;

	void Start()
	{
		player = (Player) FindObjectOfType(typeof(Player));
	}

	void LateUpdate()
	{
		if (player.isAlive)
		{
			//add position in vector by uvAnim in time
			offset += uvAnimSideAndSpeed * Time.deltaTime;

			if (offset.x > 0.2f)
				offset.x = 0;

			//so change the offset in time we calculate above
			GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
		}
	}
}

