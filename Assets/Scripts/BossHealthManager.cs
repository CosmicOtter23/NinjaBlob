using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : MonoBehaviour {

	public int bossHealth;

	public GameObject deathEffect;

	public int pointsOnDeath;

	public GameObject bossPrefab;

	public float minSize;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (bossHealth <= 1) {
			Instantiate (deathEffect, transform.position, transform.rotation);
			ScoreManager.AddPoints (pointsOnDeath);

			if (transform.localScale.y > minSize) {
				GameObject clone1 = Instantiate(bossPrefab, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
				GameObject clone2 = Instantiate(bossPrefab, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), transform.rotation) as GameObject;

				clone1.transform.localScale = new Vector3 (transform.localScale.y * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
				clone1.GetComponent<BossPatrol> ().moveSpeed = GetComponent<BossPatrol> ().moveSpeed * 1.2f;
				clone1.GetComponent<BossHealthManager> ().bossHealth = 25;

				clone2.transform.localScale = new Vector3 (transform.localScale.y * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
				clone2.GetComponent<BossPatrol> ().moveSpeed = GetComponent<BossPatrol> ().moveSpeed * 1.2f;
				clone2.GetComponent<BossHealthManager> ().bossHealth = 25;
			}

			Destroy (gameObject);
		}

	}

	public void giveDamage(int damageToGive)
	{
		bossHealth -= damageToGive;
		GetComponent<AudioSource>().Play ();
	}
}
