using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {

    public string nextLevel;
    
	void OnPress(){
		Application.LoadLevel(nextLevel);
        Time.timeScale = 1f;
        GameState.state.ResetGameState();
	}
}
