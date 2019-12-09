using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {
	public float destroyDelay;

	//THis destroys particles after some time after instantiating them
	void Start () {
		Destroy(gameObject, destroyDelay);	
	}

}
