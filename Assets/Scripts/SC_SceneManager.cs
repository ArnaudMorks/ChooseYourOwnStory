using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_SceneManager : MonoBehaviour
{
	private string currentSceneName;

	private void Start()
	{
		Scene currentScene = SceneManager.GetActiveScene(); //haalt huidige scene op
		currentSceneName = currentScene.name;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			RestartStory();
	}

	public void RestartStory()
	{
		SceneManager.LoadScene(currentSceneName);
	}

}
