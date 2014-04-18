using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lives : MonoBehaviour {

	public Transform lifeLocation;
	public GameObject lifePrefab;
	public int currentLives;
	List<GameObject> livesList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		UIPanel panel = gameObject.GetComponent<UIPanel> ();
		currentLives = GameState.STARTING_LIVES;
		Vector3 lifePosition = lifeLocation.position;
		Vector3 lifeDimension = new Vector3 (100, 100, 100);
		for (int i = 0; i < currentLives; i++) {
			GameObject life = NGUITools.AddChild(panel.gameObject,lifePrefab);
			life.transform.localScale = lifeDimension;
			life.transform.position = lifePosition;
			livesList.Add(life);
			Vector3 temp = lifePosition;
			temp.x += 0.1f;
			lifePosition = temp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameState.state.Lives != currentLives) {
			currentLives = GameState.state.Lives;
			GameObject temp = livesList[livesList.Count -1 ];
			livesList.RemoveAt(livesList.Count -1);
			Destroy (temp);
		}
	}
}
