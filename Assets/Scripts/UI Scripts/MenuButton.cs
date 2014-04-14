using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	void OnPress(){
		Application.LoadLevel("MainMenu");
	}
}
