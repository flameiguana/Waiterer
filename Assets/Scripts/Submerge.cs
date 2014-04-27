using UnityEngine;
using System.Collections;

public class Submerge : MonoBehaviour {

	public float submergeTime = 1.4f;
	float timer;

	SpriteRenderer spriteRenderer;
	public bool submerged = false;
	// Use this for initialization 
	Color originalColor;
	void Start () {
		timer = submergeTime;
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;
	}
	bool warned = false;
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer <= .35f && !warned && !submerged){
			warned = true;
			Color color = originalColor;
			color.g =  0f;	
			color.r = 1f;
			color.b = 0f;

			spriteRenderer.color = color;
		}

		if(timer <= 0f){
			timer = submergeTime;
			//Switch between full color and fully visible/
			Color color = originalColor;

			submerged = !submerged;
			warned = false;
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
