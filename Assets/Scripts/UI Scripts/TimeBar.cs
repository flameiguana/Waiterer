using UnityEngine;
using System.Collections;

public class TimeBar : MonoBehaviour {
	public float time;
	public UISlider timeBar;


	// Use this for initialization
	void Start () {
		 time = 10;
		 timeBar = gameObject.GetComponent<UISlider> ();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		timeBar.sliderValue = time / 10;
	}
}
