using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldComplete : MonoBehaviour {

	public bool playerInZone;

	private LevelSelectManager selector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical") > 0 && playerInZone) {
			selector.CompletedWorld ();
		}
	}
}
