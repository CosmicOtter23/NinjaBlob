using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour {

	private LifeManager lifeSystem;

	public GameObject coinParticle;

	public GameObject pickup;

	// Use this for initialization
	void Start () {
		lifeSystem = FindObjectOfType<LifeManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player") {
			lifeSystem.GiveLife ();
			Instantiate (coinParticle, pickup.transform.position, pickup.transform.rotation);
			Destroy (gameObject);
		}
	}

}
