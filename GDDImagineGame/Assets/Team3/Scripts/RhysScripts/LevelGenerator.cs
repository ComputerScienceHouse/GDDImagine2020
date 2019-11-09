using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
	// A list of all scenes
	List<Scene> scenes;
	
	// Start is called before the first frame update
    void Start()
    {
		scenes = new List<Scene>();

		// Makes a list of all scenes
		for(int i = 0; i < SceneManager.sceneCount; i++)
		{
			scenes.Add(SceneManager.GetSceneAt(i));
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	/// <summary>
	/// Loads the next level (if it exists)
	/// </summary>
	void NextLevel()
	{
		// If there is no next scene, nothing happens
		if(DoesNextLevelExist(SceneManager.GetActiveScene()))
		{
			// If a next scene exists, it is loaded and becomes the active scenes
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	/// <summary>
	/// A method that checks to see if a next level exists
	/// </summary>
	/// <param name="currentScene">The current scene that the game is on</param>
	/// <returns>Returns a bool on whether a next level exists</returns>
	bool DoesNextLevelExist(Scene currentScene)
	{
		int currentSceneIndex = currentScene.buildIndex;

		if(scenes[currentSceneIndex + 1] != null)
			return true;

		return false;
	}
}
