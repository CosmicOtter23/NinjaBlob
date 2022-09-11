using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	public int healthToGive;

	public GameObject healthParticle;

	public GameObject pickup;

	void OnTriggerEnter2D (Collider2D other){
		if (other.GetComponent<PlayerController> () == null)
			return;

		HealthManager.HurtPlayer (-healthToGive);

		Instantiate (healthParticle, pickup.transform.position, pickup.transform.rotation);

		Destroy (gameObject);
	}

}
