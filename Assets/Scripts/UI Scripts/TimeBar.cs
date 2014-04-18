using UnityEngine;
using System.Collections;

public class TimeBar : MonoBehaviour {
    public static TimeBar timer;
	public int levelTimeLimit = 20;
    public float time;
	public UISlider timeBar;
    public bool alarmRung = false;
    public AudioClip alarmSound;


	// Use this for initialization
	void Start () {
		 time = levelTimeLimit;
		 timeBar = gameObject.GetComponent<UISlider> ();
         
         if(timer != null) {
            Debug.LogError("There can never be two pause menus... Something went terribly wrong");
            return;
        }
        timer = this;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		timeBar.sliderValue = time / levelTimeLimit;
        if(time <=10 && !alarmRung){
            audio.PlayOneShot(alarmSound);
            alarmRung = true;
        }else if (time<=0){
            GameState.state.TimeOut();
            Destroy(this);
        }
	}
    
    	//Calculates score based on time left
	public float calculateScore (){
		float score = time * 10;
		return score;
	}
}
