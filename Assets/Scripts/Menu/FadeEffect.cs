using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour {
	//fade screen attached to the camera on the menu screen. 
	public Image BlackScreen;

	// Use this for initialization
	void Start () {
		BlackScreen.canvasRenderer.SetAlpha (0f);
	}

	//fade into black
	public void FadeIn(){
		BlackScreen.canvasRenderer.SetAlpha (0f);
		BlackScreen.CrossFadeAlpha (1f, 1f, false);
	}

	//fade out of black
	public void FadeOut(){
		BlackScreen.canvasRenderer.SetAlpha (1f);
		BlackScreen.CrossFadeAlpha (0f, 1f, false);
	}
}
