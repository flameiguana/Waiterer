using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {
	void OnPress(){
		Application.LoadLevel("Credits");
		Time.timeScale = 1f;
	}
}
