using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	public int PointsToAdd;

	public GameObject coinParticle;

	public GameObject coin;

	//public AudioSource coinSoundEffect;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<PlayerController> () == null)
			return;
		
		Instantiate (coinParticle, coin.transform.position, coin.transform.rotation);

		ScoreManager.AddPoints (PointsToAdd);

		//coinSoundEffect.Play ();

		GetComponent<AudioSource>().Play ();

		Destroy (gameObject);
	}
}
