using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	void OnPress(){
		Application.LoadLevel("Game");
	}
}
