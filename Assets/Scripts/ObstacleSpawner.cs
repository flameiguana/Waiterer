using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject unit;

	struct ObstacleInfo{
		public int row;
		public bool leftSide;
		public float desiredSpeed;
		public float spawnDelay;
		public GameObject obstaclePrefab;
	}

	float[] rowYPositions;


	// Use this for initialization
	void Start () {
		float bottomRowY = transform.position.y;
		float unitLength = unit.gameObject.renderer.bounds.size.y;
		print (unitLength);
		float height = Camera.main.orthographicSize * 2;
		//Test generation of rows.
		for(int i = 0; i < 10; i++){
			float yPosition = bottomRowY + (i * unitLength);
			Vector3 position = new Vector3(transform.position.x, yPosition);
			Instantiate(unit, position, Quaternion.identity);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
