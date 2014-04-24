using UnityEngine;
using System.Collections;

public class Submerge : MonoBehaviour {

	public float submergeTime = 1.5f;
	float timer;

	SpriteRenderer spriteRenderer;
	public bool submerged = false;
	// Use this for initialization
	void Start () {
		timer = submergeTime;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0f){
			timer = submergeTime;
			//Switch between full color and fully visible/
			Color color = spriteRenderer.color;


			submerged = !submerged;
			if(submerged){
				color.a = .3f;
			}
			else{
				color.a = 1f;
			}

			spriteRenderer.color = color;
			//collider is enabled if raft is not submerged.
			collider2D.enabled = !submerged;
		}
	}
}
