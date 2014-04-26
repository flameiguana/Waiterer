using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu pauseMenu;
    private GameObject menuPanel;
    public GameObject resumeButton;
    public GameObject nextButton;

	public void PauseToggle(){
		GameState.state.PauseToggle();
		if(GameState.state.Paused){
			Time.timeScale = 0f;
            menuPanel.SetActive(true); 
		}
		else {
			Time.timeScale = 1f;
            menuPanel.SetActive(false);
		}
	}
    
    public void GameOverMenu(){
        menuPanel.SetActive(true);
        resumeButton.SetActive(false);
    }
    
    public void LevelCompleteMenu(){
        menuPanel.SetActive(true);
        resumeButton.SetActive(false);
        nextButton.SetActive(true);
    }


	void Start () {
        // grab menu for enabling/disabling
        menuPanel = GameObject.Find("PausePanel");
        resumeButton = GameObject.Find("ResumeButton");
        nextButton = GameObject.Find("NextButton");
        nextButton.SetActive(false);
        menuPanel.SetActive(false);
        
        // set singleton menu, maybe expand this to game state later?
        if(pauseMenu != null) {
            //Debug.LogError("There can never be two pause menus... Something went terribly wrong");
            return;
        }
        pauseMenu = this;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			PauseToggle();
		}
	}
}
