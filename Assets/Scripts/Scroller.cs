﻿using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	public float speed;
	Vector3 displacement = Vector3.zero;
	
	// Use this for initialization
	void Start (){

	}
	
	void Update () {
		displacement.x = speed * Time.deltaTime;
		transform.Translate(displacement);

		if(transform.position.x < -4f || transform.position.x > 4.5f){
			Destroy(gameObject);
		}
	}
}
