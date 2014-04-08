using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public float speed = 20;
    public float hopDistance = 0.4f;
    public GameObject waiterPrefab;
    public Transform startPoint;
    
    public int lives = 3;
    public int drinksDelivered = 0;
    public bool gameOver = false;
    public bool levelComplete = false;
    
    private int score = 0;
    
    private float lastHeightReached;
    
    private Vector2 targetPosition;
    
    private bool hopping = false;
    
    //Placeholder UI
    public GUIStyle textStyle;
    public GUIStyle resultTextStyle;
    
	// Use this for initialization
	void Start () {
        targetPosition = transform.position;
        lastHeightReached = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("in update");
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
            // check if target is out of bounds
            if(( Mathf.Abs(targetPosition.x) > 4) || (targetPosition.y < -4)){
                //reset the target and don't hop
                targetPosition = transform.position;
                hopping = false;
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
    
    public void OnGUI() { 
        GUI.Label(new Rect(Screen.width/4 + 10, 5, 20, 10), "Lives = "+lives, textStyle);
        GUI.Label(new Rect(Screen.width/4 + 10, 25, 20, 10), "Score = "+score, textStyle);
        if (gameOver){
            GUI.Label(new Rect(Screen.width/3, Screen.height/2, 20, 10), "YOU SUCK", resultTextStyle);
        }else if(levelComplete){
            GUI.Label(new Rect(Screen.width/3, Screen.height/2, 20, 10), "U DA BESSSSSSSSSSS", resultTextStyle);
        }
    }
    
    void FixedUpdate() {
        
    }
    // Check for any collisions
     void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Finish"){ //create a new waiter at the starting point
            GameObject newWaiterer = (GameObject)Instantiate(waiterPrefab, transform.position, transform.rotation);
            resetWaiter();
            drinksDelivered++;
            score += 100;
            if(drinksDelivered == 5){
                //score += secondsLeft;
                levelComplete = true;
            }
            
        }else if (other.gameObject.tag == "Obstacle"){
            resetWaiter();
            lives--;
            if(lives == 0){
                gameOver = true;
            }
        }
    }
    public void resetWaiter(){
        hopping = false;
        transform.position = startPoint.position;
        targetPosition = transform.position;
        lastHeightReached = transform.position.y;
    }
}
