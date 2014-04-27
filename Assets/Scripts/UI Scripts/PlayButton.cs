using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
    
    public string level;
    
	void OnClick(){
		if(GameState.state != null){
			GameState.state.ResetGameState();
			Time.timeScale = 1f;
		}
		Application.LoadLevel(level);
	}
}
