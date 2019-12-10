using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	//provides functions for quit and start buttons to access
	public GameObject start;
	public GameObject Instructions;
	public GameObject Quit;


	public void PlayGameDelayed(){
		Invoke ("PlayGame", 2f);
	}

	public void PlayGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void quit () {
		Application.Quit ();
	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			quit ();
		}
	}
}
