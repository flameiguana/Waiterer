using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour {
    GameObject pauseMenu;

	void OnPress(){
		Time.timeScale = 1f;
        PauseMenu.pauseMenu.PauseToggle();
	}
    
    void Start () {
		
	}
}
