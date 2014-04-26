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
                ObstacleInfo rowOne2 = new ObstacleInfo(stripEnemyPrefab, 1);
                rowOne2.leftSide = true;
                rowOne2.desiredSpeed = 2f;
                rowOne2.spawnDelay = 4f;

                obstacleInfoList.Add(rowOne2);
                //Note rows 7 through 11 should spawn platforms
                ObstacleInfo row72 = new ObstacleInfo(stripPlatformPrefab, 7);
                row72.leftSide = true;
                row72.desiredSpeed = 1.2f;
                row72.spawnDelay = 1.5f;
                row72.EnableSubmersion(3);
                
                obstacleInfoList.Add(row72);
                
                ObstacleInfo row82 = new ObstacleInfo(stripPlatformPrefab, 8);
                row82.leftSide = false;
                row82.desiredSpeed = 1.2f;
                row82.spawnDelay = 3f;
                row82.SpawnSpecialItem(CashMoney, 8);
                
                obstacleInfoList.Add(row82);
                
                ObstacleInfo row92 = new ObstacleInfo(sixUnitPlatformPrefab, 9);
                row92.leftSide = false;
                row92.desiredSpeed = 2f;
                row92.spawnDelay = 2f;
                
                obstacleInfoList.Add(row92);
                
                ObstacleInfo row102 = new ObstacleInfo(twoUnitPlatformPrefab, 10);
                row102.leftSide = true;
                row102.desiredSpeed = 1.2f;
                row102.spawnDelay = 1.2f;
                
                obstacleInfoList.Add(row102);
                
                ObstacleInfo row112 = new ObstacleInfo(stripPlatformPrefab, 11);
                row112.leftSide = false;
                row112.desiredSpeed = 1.2f;
                row112.spawnDelay = 1.8f;
                row112.SpawnSpecialItem(CashMoney, 10);
                row112.timesSpawned = 5;
                obstacleInfoList.Add(row112);
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
