using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour {

	public GameObject unit;
	int ROWS = 12;


	public GameObject stripPrefab;



	class PlatformInfo{
		public int row;
		public bool leftSide = true;
		public float desiredSpeed = 5f;
		public float spawnDelay = 5f;
		public GameObject obstaclePrefab;

		public float timeLeft;

		public PlatformInfo(GameObject obstaclePrefab, int row){
			this.obstaclePrefab = obstaclePrefab;
			this.row = row;
			timeLeft = 0f;
		}

		public void TickTimer(float timePassed){
			timeLeft -= timePassed;
		}

		public void ResetTimer(){
			timeLeft = spawnDelay;
		}
	}

	List<PlatformInfo> PlatformInfoList;

	float[] rowYPositions;
	float leftXPosition = -3.2f;
	float rightXPosition = 3.2f;


	void Awake(){
		rowYPositions = new float[ROWS];
		PlatformInfoList = new List<PlatformInfo>();
	}

	// Use this for initialization
	void Start () {
		float bottomRowY = transform.position.y;
		float unitLength = unit.gameObject.renderer.bounds.size.y;
		//print (unitLength);

		//Test generation of rows.
		for(int i = 0; i < ROWS; i++){
			float yPosition = bottomRowY + (i * unitLength);
			rowYPositions[i] = yPosition;

			//For visualizing.
			//Vector3 position = new Vector3(transform.position.x, yPosition);
			//Instantiate(unit, position, Quaternion.identity);
		}

		//Level design:
		//Note rows 7 through 11 should spawn platforms
		PlatformInfo row7 = new PlatformInfo(stripPrefab, 7);
		row7.leftSide = true;
		row7.desiredSpeed = 2f;
		row7.spawnDelay = 2f;

		PlatformInfoList.Add(row7);
        
        PlatformInfo row8 = new PlatformInfo(stripPrefab, 8);
		row8.leftSide = false;
		row8.desiredSpeed = 3f;
		row8.spawnDelay = 1f;

		PlatformInfoList.Add(row8);
        
        PlatformInfo row9 = new PlatformInfo(stripPrefab, 9);
		row9.leftSide = true;
		row9.desiredSpeed = 2f;
		row9.spawnDelay = 1.5f;

		PlatformInfoList.Add(row9);
        
        PlatformInfo row10 = new PlatformInfo(stripPrefab, 10);
		row10.leftSide = false;
		row10.desiredSpeed = 2f;
		row10.spawnDelay = 1f;

		PlatformInfoList.Add(row10);
        
        PlatformInfo row11 = new PlatformInfo(stripPrefab, 11);
		row11.leftSide = true;
		row11.desiredSpeed = 2f;
		row11.spawnDelay = 1f;

		PlatformInfoList.Add(row11);
	}
	

	void Update () {
		//Iterate through every obstacle info .
		foreach(PlatformInfo info in PlatformInfoList){
			//pass in time since last frame
			info.TickTimer(Time.deltaTime);
			//if it is time to respawn object, reset timer and instantiate obstacle
			if(info.timeLeft <= 0f){
				info.ResetTimer();

				float xPosition = rightXPosition;
				if(info.leftSide)
					xPosition = leftXPosition;

				Vector3 position = new Vector3(xPosition, rowYPositions[info.row]);
				GameObject obstacle = Instantiate(info.obstaclePrefab, position, Quaternion.identity) as GameObject;
				//if spawning from right, go left, vice versa
				float sign = -1f;
				if(info.leftSide)
					sign = 1f;

				obstacle.GetComponent<Scroller>().speed = info.desiredSpeed * sign;
			}
		}
	}

}
