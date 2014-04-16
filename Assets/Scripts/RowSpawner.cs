using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowSpawner : MonoBehaviour {

	public GameObject unit;
	int ROWS = 12;


	public GameObject stripEnemyPrefab;
	public GameObject twoUnitEnemyPrefab;
	public GameObject stripPlatformPrefab;
	public GameObject sixUnitPlatformPrefab;


	class ObstacleInfo{
		public int row;
		public bool leftSide = true;
		public float desiredSpeed = 5f;
		public float spawnDelay = 5f;
		public GameObject obstaclePrefab;

		public float timeLeft;

		public ObstacleInfo(GameObject obstaclePrefab, int row){
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

	List<ObstacleInfo> obstacleInfoList;

	float[] rowYPositions;
	float leftXPosition = -3.2f;
	float rightXPosition = 3.2f;


	void Awake(){
		rowYPositions = new float[ROWS];
		obstacleInfoList = new List<ObstacleInfo>();
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
		//Note rows 0, 6 shouldn't  spawn anything
		ObstacleInfo rowOne = new ObstacleInfo(stripEnemyPrefab, 1);
		rowOne.leftSide = true;
		rowOne.desiredSpeed = 2f;
		rowOne.spawnDelay = 4f;

		obstacleInfoList.Add(rowOne);
        
        ObstacleInfo rowTwo = new ObstacleInfo(stripEnemyPrefab, 2);
		rowTwo.leftSide = false;
		rowTwo.desiredSpeed = 1f;
		rowTwo.spawnDelay = 4f;

		obstacleInfoList.Add(rowTwo);
        
        ObstacleInfo rowThree = new ObstacleInfo(stripEnemyPrefab, 3);
		rowThree.leftSide = true;
		rowThree.desiredSpeed = 1f;
		rowThree.spawnDelay = 4f;

		obstacleInfoList.Add(rowThree);
        
        ObstacleInfo rowFour = new ObstacleInfo(twoUnitEnemyPrefab, 4);
		rowFour.leftSide = true;
		rowFour.desiredSpeed = 2f;
		rowFour.spawnDelay = 3f;

		obstacleInfoList.Add(rowFour);
        
        ObstacleInfo rowFive = new ObstacleInfo(stripEnemyPrefab, 5);
		rowFive.leftSide = false;
		rowFive.desiredSpeed = 1.5f;
		rowFive.spawnDelay = 4f;

		obstacleInfoList.Add(rowFive);

		//Note rows 7 through 11 should spawn platforms
		ObstacleInfo row7 = new ObstacleInfo(stripPlatformPrefab, 7);
		row7.leftSide = true;
		row7.desiredSpeed = 1f;
		row7.spawnDelay = 2f;
		
		obstacleInfoList.Add(row7);
		
		ObstacleInfo row8 = new ObstacleInfo(stripPlatformPrefab, 8);
		row8.leftSide = false;
		row8.desiredSpeed = 3f;
		row8.spawnDelay = 1f;
		
		obstacleInfoList.Add(row8);
		
		ObstacleInfo row9 = new ObstacleInfo(sixUnitPlatformPrefab, 9);
		row9.leftSide = true;
		row9.desiredSpeed = 2f;
		row9.spawnDelay = 2f;
		
		obstacleInfoList.Add(row9);
		
		ObstacleInfo row10 = new ObstacleInfo(stripPlatformPrefab, 10);
		row10.leftSide = false;
		row10.desiredSpeed = 2f;
		row10.spawnDelay = 1f;
		
		obstacleInfoList.Add(row10);
		
		ObstacleInfo row11 = new ObstacleInfo(stripPlatformPrefab, 11);
		row11.leftSide = true;
		row11.desiredSpeed = 1.5f;
		row11.spawnDelay = 1.5f;
		
		obstacleInfoList.Add(row11);
	}
	

	void Update () {
		//Iterate through every obstacle info.
		foreach(ObstacleInfo info in obstacleInfoList){
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
