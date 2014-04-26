using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {
    
    public string level;
    
	void OnPress(){
		Application.LoadLevel(level);
        Time.timeScale = 1f;
	}
}
