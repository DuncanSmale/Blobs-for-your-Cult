using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseScreen;
	public bool GamePaused = false;
	public Button resume;

	void Pause(){
		PauseScreen.SetActive (true);
		Time.timeScale = 0f;				//freezes the game
		GamePaused = true;
		
	}

	public void Resume(){
		PauseScreen.SetActive (false);
		Time.timeScale = 1f;				//resumes the game
		GamePaused = false;
	}

	public void MainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Game");
	}
	
	public void Quit(){
		Application.Quit ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GamePaused == true){
				Resume ();
			}else{
				Pause ();
			}
		}
	}
}
