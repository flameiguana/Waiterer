﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject unit;
	int ROWS = 12;


	public GameObject stripPrefab;



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
		ObstacleInfo rowOne = new ObstacleInfo(stripPrefab, 1);
		rowOne.leftSide = true;
		rowOne.desiredSpeed = 2f;
		rowOne.spawnDelay = 4f;

		obstacleInfoList.Add(rowOne);
        
        ObstacleInfo rowTwo = new ObstacleInfo(stripPrefab, 2);
		rowTwo.leftSide = false;
		rowTwo.desiredSpeed = 1f;
		rowTwo.spawnDelay = 4f;

		obstacleInfoList.Add(rowTwo);
        
        ObstacleInfo rowThree = new ObstacleInfo(stripPrefab, 3);
		rowThree.leftSide = true;
		rowThree.desiredSpeed = 1f;
		rowThree.spawnDelay = 4f;

		obstacleInfoList.Add(rowThree);
        
        ObstacleInfo rowFour = new ObstacleInfo(stripPrefab, 4);
		rowFour.leftSide = true;
		rowFour.desiredSpeed = 2f;
		rowFour.spawnDelay = 3f;

		obstacleInfoList.Add(rowFour);
        
        ObstacleInfo rowFive = new ObstacleInfo(stripPrefab, 5);
		rowFive.leftSide = false;
		rowFive.desiredSpeed = 1.5f;
		rowFive.spawnDelay = 4f;

		obstacleInfoList.Add(rowFive);
	}
	

	void Update () {
		//Iterate through every obstacle info .
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
