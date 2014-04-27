using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowSpawner : MonoBehaviour {

    public int gameLevel = 0;
	public GameObject unit;
	int ROWS = 12;


	public GameObject stripEnemyPrefab;
    public GameObject stripEnemyLeftPrefab;
	public GameObject twoUnitEnemyPrefab;
	public GameObject stripPlatformPrefab;
    public GameObject twoUnitPlatformPrefab;
	public GameObject sixUnitPlatformPrefab;

	public GameObject CashMoney;

	class ObstacleInfo{
		public int row;
		public bool leftSide = true;
		public float desiredSpeed = 5f;
		public float spawnDelay = 5f;
		public GameObject obstaclePrefab;

		public float timeLeft;
		public int timesSpawned = 0;

		public int submersionFrequency = 0;

		public int specialItemFrequency = 0;
		public GameObject specialItem;

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

		public void EnableSubmersion(int frequency){
			submersionFrequency = frequency;
		}

		public void SpawnSpecialItem(GameObject item, int frequency){
			specialItem = item;
			specialItemFrequency = frequency;
		}

	}

	List<ObstacleInfo> obstacleInfoList;

	float[] rowYPositions;
	float leftXPosition = -4.2f;
	float rightXPosition = 4.2f;


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
        
        switch(gameLevel){
            case 0:
                //Level design:
                //Note rows 0, 6 shouldn't  spawn anything
                ObstacleInfo rowOne = new ObstacleInfo(stripEnemyPrefab, 1);
                rowOne.leftSide = true;
                rowOne.desiredSpeed = 2f;
                rowOne.spawnDelay = 4f;

                obstacleInfoList.Add(rowOne);
                
                ObstacleInfo rowTwo = new ObstacleInfo(stripEnemyLeftPrefab, 2);
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
                
                ObstacleInfo rowFive = new ObstacleInfo(stripEnemyLeftPrefab, 5);
                rowFive.leftSide = false;
                rowFive.desiredSpeed = 1.5f;
                rowFive.spawnDelay = 4f;

                obstacleInfoList.Add(rowFive);

                //Note rows 7 through 11 should spawn platforms
                ObstacleInfo row7 = new ObstacleInfo(stripPlatformPrefab, 7);
                row7.leftSide = true;
                row7.desiredSpeed = 1.2f;
                row7.spawnDelay = 1.5f;
                row7.EnableSubmersion(3);
                
                obstacleInfoList.Add(row7);
                
                ObstacleInfo row8 = new ObstacleInfo(stripPlatformPrefab, 8);
                row8.leftSide = false;
                row8.desiredSpeed = 1.2f;
                row8.spawnDelay = 3f;
                row8.SpawnSpecialItem(CashMoney, 8);
                
                obstacleInfoList.Add(row8);
                
                ObstacleInfo row9 = new ObstacleInfo(sixUnitPlatformPrefab, 9);
                row9.leftSide = false;
                row9.desiredSpeed = 2f;
                row9.spawnDelay = 2f;
                
                obstacleInfoList.Add(row9);
                
                ObstacleInfo row10 = new ObstacleInfo(twoUnitPlatformPrefab, 10);
                row10.leftSide = true;
                row10.desiredSpeed = 1.2f;
                row10.spawnDelay = 1.2f;
                
                obstacleInfoList.Add(row10);
                
                ObstacleInfo row11 = new ObstacleInfo(stripPlatformPrefab, 11);
                row11.leftSide = false;
                row11.desiredSpeed = 1.2f;
                row11.spawnDelay = 1.8f;
                row11.SpawnSpecialItem(CashMoney, 10);
                row11.timesSpawned = 5;
                obstacleInfoList.Add(row11);
                
                break;
            case 1:
                ObstacleInfo row1 = new ObstacleInfo(twoUnitEnemyPrefab, 1);
                row1.leftSide = true;
                row1.desiredSpeed = 2f;
                row1.spawnDelay = 3f;

                obstacleInfoList.Add(row1);
                
                ObstacleInfo row2 = new ObstacleInfo(stripEnemyLeftPrefab, 2);
                row2.leftSide = false;
                row2.desiredSpeed = 1f;
                row2.spawnDelay = 4f;

                obstacleInfoList.Add(row2);
                
                ObstacleInfo row3 = new ObstacleInfo(stripEnemyPrefab, 3);
                row3.leftSide = true;
                row3.desiredSpeed = 1f;
                row3.spawnDelay = 4f;

                obstacleInfoList.Add(row3);
                
                ObstacleInfo row4 = new ObstacleInfo(twoUnitEnemyPrefab, 4);
                row4.leftSide = true;
                row4.desiredSpeed = 2f;
                row4.spawnDelay = 1.5f;

                obstacleInfoList.Add(row4);
                
                ObstacleInfo row5 = new ObstacleInfo(stripEnemyLeftPrefab, 5);
                row5.leftSide = false;
                row5.desiredSpeed = 1.5f;
                row5.spawnDelay = 3f;

                obstacleInfoList.Add(row5);
                //Note rows 7 through 11 should spawn platforms
                ObstacleInfo rowSeven = new ObstacleInfo(sixUnitPlatformPrefab, 7);
                rowSeven.leftSide = true;
                rowSeven.desiredSpeed = 2f;
                rowSeven.spawnDelay = 2f;
                rowSeven.SpawnSpecialItem(CashMoney, 3);
                
                obstacleInfoList.Add(rowSeven);
                
                ObstacleInfo rowEight = new ObstacleInfo(stripPlatformPrefab, 8);
                rowEight.leftSide = false;
                rowEight.desiredSpeed = 1.2f;
                rowEight.spawnDelay = 1.5f;
                rowEight.EnableSubmersion(2);
                
                obstacleInfoList.Add(rowEight);
                
                ObstacleInfo rowNine = new ObstacleInfo(sixUnitPlatformPrefab, 9);
                rowNine.leftSide = false;
                rowNine.desiredSpeed = 2f;
                rowNine.spawnDelay = 2f;
                
                obstacleInfoList.Add(rowNine);
                
                ObstacleInfo rowTen = new ObstacleInfo(stripPlatformPrefab, 10);
                rowTen.leftSide = true;
                rowTen.desiredSpeed = 1.2f;
                rowTen.spawnDelay = 1.2f;
                rowTen.EnableSubmersion(3);
                
                obstacleInfoList.Add(rowTen);
                
                ObstacleInfo rowEleven = new ObstacleInfo(twoUnitPlatformPrefab, 11);
                rowEleven.leftSide = false;
                rowEleven.desiredSpeed = 1.2f;
                rowEleven.spawnDelay = 1.2f;
                rowEleven.SpawnSpecialItem(CashMoney, 10);
                rowEleven.timesSpawned = 5;
                obstacleInfoList.Add(rowEleven);
                break;
            default:
                ObstacleInfo rowOne3 = new ObstacleInfo(stripEnemyPrefab, 1);
                rowOne3.leftSide = true;
                rowOne3.desiredSpeed = 2f;
                rowOne3.spawnDelay = 4f;

                obstacleInfoList.Add(rowOne3);
                
                //Note rows 7 through 11 should spawn platforms
                ObstacleInfo row73 = new ObstacleInfo(stripPlatformPrefab, 7);
                row73.leftSide = true;
                row73.desiredSpeed = 1.2f;
                row73.spawnDelay = 1.5f;
                row73.EnableSubmersion(3);
                
                obstacleInfoList.Add(row73);
                
                ObstacleInfo row74 = new ObstacleInfo(stripPlatformPrefab, 8);
                row74.leftSide = false;
                row74.desiredSpeed = 1.2f;
                row74.spawnDelay = 3f;
                row74.SpawnSpecialItem(CashMoney, 8);
                
                obstacleInfoList.Add(row74);
                
                ObstacleInfo row93 = new ObstacleInfo(sixUnitPlatformPrefab, 9);
                row93.leftSide = false;
                row93.desiredSpeed = 2f;
                row93.spawnDelay = 2f;
                
                obstacleInfoList.Add(row93);
                
                ObstacleInfo row103 = new ObstacleInfo(twoUnitPlatformPrefab, 10);
                row103.leftSide = true;
                row103.desiredSpeed = 1.2f;
                row103.spawnDelay = 1.2f;
                
                obstacleInfoList.Add(row103);
                
                ObstacleInfo row113 = new ObstacleInfo(stripPlatformPrefab, 11);
                row113.leftSide = false;
                row113.desiredSpeed = 1.2f;
                row113.spawnDelay = 1.8f;
                row113.SpawnSpecialItem(CashMoney, 10);
                row113.timesSpawned = 5;
                obstacleInfoList.Add(row113);
                break;
            }
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
				info.timesSpawned++;
				//if spawning from right, go left, vice versa
				float sign = -1f;
				if(info.leftSide)
					sign = 1f;

				obstacle.GetComponent<Scroller>().speed = info.desiredSpeed * sign;
				//just werks
				if(info.submersionFrequency > 0 && info.timesSpawned % info.submersionFrequency == 0){
					obstacle.AddComponent<Submerge>();
				}
				if(info.specialItemFrequency > 0 && info.timesSpawned % info.specialItemFrequency == 0){
					GameObject specialItem = Instantiate (info.specialItem, position, Quaternion.identity) as GameObject;
					specialItem.transform.parent = obstacle.transform;
				}
			}
		}
	}

}
