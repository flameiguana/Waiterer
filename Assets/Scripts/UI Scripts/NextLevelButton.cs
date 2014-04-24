using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {

	void OnPress(){
		Application.LoadLevel("Game");
        Time.timeScale = 1f;
	}
}
