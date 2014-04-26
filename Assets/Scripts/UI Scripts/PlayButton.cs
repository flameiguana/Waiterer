using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
    
    public string level;
    
	void OnPress(){
		Application.LoadLevel(level);
        GameState.state.ResetGameState();
        Time.timeScale = 1f;
	}
}
