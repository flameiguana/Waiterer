using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	//keep game  state here for now
	public bool paused;

	void PauseToggle(){
		paused = !paused;
		if(paused){
			Time.timeScale = 0f;
		}
		else {
			Time.timeScale = 1f;
		}
	}


	void Start () {
		paused = false;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauseToggle();
		}
	}
}
