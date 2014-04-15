using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public float speed = 40;
    public float hopDistance = 0.4f;
    public GameObject waiterPrefab;
    public Transform startPoint;
    
    public int lives = 3;
    public int drinksDelivered = 0;
    public bool gameOver = false;
    public bool levelComplete = false;
    public bool onPlatform = false;
	//the amount of fixedupdates that the player is detected to be in water
	float inWaterFrames = 0;
    public float waterStartingY = 0.00f;
    
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
        if(!hopping){ //   
            if(Input.GetKeyDown(KeyCode.W)){
                targetPosition.y = transform.position.y + hopDistance;
                adjustForPlatformX();
                hopping =true;
            }else if (Input.GetKeyDown(KeyCode.A)){
                targetPosition.x = transform.position.x - hopDistance;
                adjustForPlatformY();
                hopping = true;
            }else if (Input.GetKeyDown(KeyCode.S)){
                targetPosition.y = transform.position.y - hopDistance;
                adjustForPlatformX();
                hopping = true;
            }else if (Input.GetKeyDown(KeyCode.D)){
                targetPosition.x = transform.position.x + hopDistance;
                adjustForPlatformY();
                hopping = true;
            }
            // check if target is out of bounds
            if(( Mathf.Abs(targetPosition.x) > 2.8) || (targetPosition.y < -2.8)){
                //reset the target and don't hop
                targetPosition = transform.position;
                hopping = false;
            }
        } else if(hopping){
            // if hopping, then transition to the target location
            if((Vector3.Distance(transform.position,targetPosition)>0.005)){
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            } else {
                // stop hopping to allow for a new move to be made
                hopping = false;
                // increase the score if a new max height has been reached
                if ((transform.position.y - lastHeightReached)> 0.006){
                    score += 10;
                    lastHeightReached = transform.position.y; 
                }
                
                //Debug.Log("Score = "+score);
            }
        }
	}
    
    public void OnGUI() { 
        GUI.Label(new Rect(10, 5, 20, 10), "Lives = "+lives, textStyle);
        GUI.Label(new Rect(10, 25, 20, 10), "Score = "+score, textStyle);
        if (gameOver){
            PauseMenu.pauseMenu.GameOverMenu();
            GUI.Label(new Rect(Screen.width/3, Screen.height/2, 20, 10), "YOU SUCK", resultTextStyle);
        }else if(levelComplete){
            PauseMenu.pauseMenu.GameOverMenu();
            GUI.Label(new Rect(Screen.width/3, Screen.height/2, 20, 10), "U DA BESSSSSSSSSSS", resultTextStyle);
        }
        //GUI.DrawTexture(new Rect((xOffset, yOffset, textureWidth, textureHeight, textureToDraw, ScaleMode.ScaleToFit, true);
    }
    
    void FixedUpdate() {
		//Counts you as being on water if you're in there for 2 or more frames
        if(inWaterFrames >= 2){
			print ("died to water");
			resetWaiter();
			lives--;
			if(lives == 0){
				gameOver = true;
			}
		}
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
        }else if (other.gameObject.tag == "Platform" && !hopping){
			inWaterFrames = 0;
            transform.parent = other.transform;
            hopping = false;
            onPlatform = true;
        } 
		else {
			inWaterFrames = 0;
		}
	}

	//Can't rely on trigger enter because it may miss the point where it enters.
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Platform" && !hopping){
			inWaterFrames = 0;
			onPlatform = true;
		}
		else if(other.gameObject.tag == "Water" && !hopping && !onPlatform){
			inWaterFrames++;
		}
		else {
			inWaterFrames = 0;
		}
	}
    
    public void resetWaiter(){
        hopping = false;
		inWaterFrames = 0;
        transform.position = startPoint.position;
        targetPosition = transform.position;
        lastHeightReached = transform.position.y;
    }
    
    public void adjustForPlatformX(){
        if(onPlatform){
            onPlatform = false;
            targetPosition.x = transform.position.x;
            transform.parent = null;
        }
    }
    public void adjustForPlatformY(){
        if(onPlatform){
            onPlatform = false;
            targetPosition.y = transform.position.y;
            transform.parent = null;
        }
    }
}
