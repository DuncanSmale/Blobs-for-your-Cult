using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//required when dealing with event data
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour, ISelectHandler { 
	//behaviour inherited for on select and on press events and functions

	BaseEventData buttonEvent;
	public AudioClip buttonHover;
	public AudioClip clicked;
	public EventSystem MenuSystem; 
	//public List<GameObject> Menu1 = new List<GameObject>();

	//ensures that only the back button is selected in sub menus
	//this allows users to control most functions with the keyboard
	public void Update ()
	{
		//MenuSystem.SetSelectedGameObject (Menu1 [0]);
	}

	/*There is a design error in Unity that makes the pointer highlight visible when the button is already highlighted by keyboard commands
	//this code is an attempt to avoid this error
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!EventSystem.current.alreadySelecting)
		EventSystem.current.SetSelectedGameObject(gameObject);
	}

	public void OnDeselect(BaseEventData eventData){
		gameObject.GetComponent<Selectable> ().OnPointerExit (null);
	}*/

	//do this whene UI is selected
	public void OnSelect (BaseEventData eventData) {
		SoundController.instance.RandomPitchandsfx (buttonHover);
	}
	
}
