using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public float speed = 20;
    public float hopDistance = 0.58f;
    private int score = 0;
    private float lastHeightReached;
    
    private Vector2 targetPosition;
    
    private bool hopping = false;
    
	// Use this for initialization
	void Start () {
        targetPosition = transform.position;
        lastHeightReached = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if(!hopping){ // If 
            if(Input.GetKeyDown(KeyCode.W)){
                targetPosition.y = transform.position.y + hopDistance;
                hopping =true;
            }else if (Input.GetKeyDown(KeyCode.A)){
                targetPosition.x = transform.position.x - hopDistance;
                hopping = true;
            }else if (Input.GetKeyDown(KeyCode.S)){
                targetPosition.y = transform.position.y - hopDistance;
                hopping = true;
            }else if (Input.GetKeyDown(KeyCode.D)){
                targetPosition.x = transform.position.x + hopDistance;
                hopping = true;
            }
        } else {
            // if hopping, then transition to the target location
            if((Vector3.Distance(transform.position,targetPosition)>0.005)){
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            } else {
                // stop hopping to allow for a new move to be made
                hopping = false;
                // increase the score if a new max height has been reached
                if (transform.position.y > lastHeightReached){
                    score += 10;
                    lastHeightReached = transform.position.y; 
                }
                
                Debug.Log("Score = "+score);
            }
        }
	}
    void FixedUpdate() {
        
    }
}
