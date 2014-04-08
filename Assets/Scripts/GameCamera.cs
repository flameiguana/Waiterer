using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public Sprite background;

	// Use this for initialization
	void Start () {

		//Takes the height of background sprite in world units.
		float desiredHeightUnits = background.bounds.size.y;
		//adjust camera to match this
		camera.orthographicSize = desiredHeightUnits / 2.0f;
		//print (camera.orthographicSize);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
