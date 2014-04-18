using UnityEngine;
using System.Collections;

public class TimeBar : MonoBehaviour {
	public int levelTimeLimit = 20;
    public float time;
	public UISlider timeBar;
    public bool alarmRung = false;
    public AudioClip alarmSound;


	// Use this for initialization
	void Start () {
		 time = levelTimeLimit;
		 timeBar = gameObject.GetComponent<UISlider> ();
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
}
