using UnityEngine;
using System.Collections;

public class BackToMenuButton : MonoBehaviour {

	void OnPress(){
		Application.LoadLevel("MainMenu");
		Time.timeScale = 1f;
	}
}
