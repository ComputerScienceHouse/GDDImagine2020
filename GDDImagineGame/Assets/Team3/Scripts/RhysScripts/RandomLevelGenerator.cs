using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
	blank,
	obstacle,
	coin,
	powerup
}

public class RandomLevelGenerator : MonoBehaviour
{
	public GameObject floorPrefab;
	GameObject newFloor;

	public GameObject obstaclePrefab;
	public GameObject coinPrefab;
	public GameObject powerupPrefab;
	public Element[,] generation;
	public int width;
	public int length;
	public float widthStart;
	public float lengthStart;
	public float widthSpread;
	public float lengthSpread;
	public float blankPercent;
	public float obstaclePercent;
	public float coinPercent;
	public float powerupPercent;

	// Start is called before the first frame update
	void Start()
	{
		float totalWidth = widthStart + (widthSpread * width);
		float totalLength = lengthStart + (lengthSpread * length);

		newFloor = Instantiate(floorPrefab, new Vector3(totalWidth / 2, 0.5f, totalLength / 2), Quaternion.identity);
		newFloor.transform.localScale = new Vector3(
											totalWidth,
											newFloor.transform.localScale.y,
											totalLength);

		// floor.transform.position.x = widthStart + (widthSpread * width);
		// floor.transform.position.z = lengthStart + (lengthSpread + length);
		generation = new Element[width, length];        // Creates a new array
		generation = GenerateRandomLevel(generation);   // Generates a random level and stores it in the array
		DrawLevel(generation);                          // Draws the level in the scene
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Loops through a filled array and draws a gameObject for each index, 
	/// depending on the element specified in each index
	/// </summary>
	/// <param name="randomLevelArray">A filled 2D array</param>
	public void DrawLevel(Element[,] randomLevelArray)
	{
		Element element;

		for(int w = 0; w < randomLevelArray.GetLength(0); w++)
		{
			for(int h = 0; h < randomLevelArray.GetLength(1); h++)
			{
				element = randomLevelArray[w, h];

				switch(element)
				{
					// Nothing is spawned when it is a blank element
					case Element.blank:
						break;
					// Instantiates an obstacle gameObject in the scene	
					case Element.obstacle:
						Instantiate(
							obstaclePrefab,
							new Vector3(
								w + widthStart + (w * widthSpread),
								0.5f,
								h + lengthStart + (h * lengthSpread)),
							Quaternion.identity);
						break;
					// Instantiates a coin gameObject in the scene
					case Element.coin:
						Instantiate(
							coinPrefab,
							new Vector3(
								w + widthStart + (w * widthSpread),
								0.5f,
								h + lengthStart + (h * lengthSpread)),
							Quaternion.identity);
						break;
					// Instantiates a powerup gameObject in the scene
					case Element.powerup:
						Instantiate(
							powerupPrefab,
							new Vector3(
								w + widthStart + (w * widthSpread),
								0.5f,
								h + lengthStart + (h * lengthSpread)),
							Quaternion.identity);
						break;
				}
			}
		}
	}

	/// <summary>
	/// Loops thr the array and assigns each index a random element
	/// </summary>
	/// <param name="elementArray">A 2D array that will be given random elements in each index</param>
	/// <returns>Returns a 2D array with a randomly generated level</returns>
	public Element[,] GenerateRandomLevel(Element[,] elementArray)
	{
		for(int w = 0; w < elementArray.GetLength(0); w++)
			for(int h = 0; h < elementArray.GetLength(1); h++)
				elementArray[w, h] = GetRandomElement();

		return elementArray;
	}

	/// <summary>
	/// A helper method used for randomly picking an element
	/// </summary>
	/// <returns>Returns a random element (based on percentages)</returns>
	Element GetRandomElement()
	{
		Element randElement;
		float randNum = Random.Range(0.0f, 100.0f);

		if(randNum <= blankPercent)                                         // % to spawn blank (nothing)
			randElement = Element.blank;
		else if(randNum <= blankPercent + obstaclePercent)                  // % to spawn an obstacle gameObject
			randElement = Element.obstacle;
		else if(randNum <= blankPercent + obstaclePercent + coinPercent)    // % to spawn a coin gameObject
			randElement = Element.coin;
		else                                                                // % to spawn a powerup gameObject
			randElement = Element.powerup;

		return randElement;
	}
}
