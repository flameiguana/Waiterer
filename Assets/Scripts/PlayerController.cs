using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    
    public float speed = 40;
    public float hopDistance = 0.4f;
    public GameObject waiterPrefab;
    public Transform startPoint;
    public AudioClip jump;
    public AudioClip crashWhistle;
    public AudioClip crashDishes;
    public AudioClip splash;
    public AudioClip laughing;
    public List<GameObject> foodDelivered = new List<GameObject>();
    
    public bool onPlatform = false;
	//the amount of fixedupdates that the player is detected to be in water
	float inWaterFrames = 0;
    public float waterStartingY = 0.00f;
    
    private float lastHeightReached;
    
    private Vector3 targetDisplacement;
    
    private bool hopping = false;
    
    //Placeholder UI
    public GUIStyle textStyle;
    public GUIStyle resultTextStyle;
    
    public int dollars;
    public int change;
    

	// Use this for initialization
	void Start () {
        lastHeightReached = transform.position.y;
		transform.localPosition = startPoint.localPosition;
	}
	Vector3 targetPosition;
	// Update is called once per frame
	void Update () {
        if(GameState.state.GameOver){
            Destroy(this);
        }
        if(!hopping){ //
			targetDisplacement = Vector3.zero;
            if(Input.GetKeyDown(KeyCode.W)){
                targetDisplacement.y = hopDistance;
                hopping =true;
            }else if (Input.GetKeyDown(KeyCode.A)){
                targetDisplacement.x = -hopDistance;
                hopping = true;
            }else if (Input.GetKeyDown(KeyCode.S)){
                targetDisplacement.y = -hopDistance;
                hopping = true;
            }else if (Input.GetKeyDown(KeyCode.D)){
                targetDisplacement.x = hopDistance;
                hopping = true;
            }
            if(hopping){
                audio.PlayOneShot(jump);
            }

			targetPosition = transform.localPosition + targetDisplacement;

            // check if target is out of bounds
			Vector3 worldPosition = transform.position + targetDisplacement;
			if((Mathf.Abs(worldPosition.x) > 2.8f) || worldPosition.y < -2.8f){
                //reset the target and don't hop
				targetPosition = transform.localPosition;
                targetDisplacement = Vector3.zero;
                hopping = false;
            }
        } 
		if(hopping){
            // if hopping, then transition to the target location
			float distance = Vector3.Distance(transform.localPosition, targetPosition);
            if(distance > 0.005f){
				transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, speed * Time.deltaTime);
            } else {
                // stop hopping to allow for a new move to be made
				transform.localPosition = targetPosition;
                hopping = false;
				onPlatform = false;
				transform.parent = null;
                // increase the score if a new max height has been reached
                if ((transform.position.y - lastHeightReached)> 0.006){
                    GameState.state.Score += 10;
                    lastHeightReached = transform.position.y; 
                }
           }
        }
	}
    
    public void OnGUI() { 
        //public int dollars;
        //public int change;
        dollars = (int) GameState.state.Score / 100;
        change = (int) GameState.state.Score % 100;
        GUI.Label(new Rect(10, 25, 20, 10), "Tips $"+dollars+"."+change, textStyle);
        
        
        //GUI.Label(new Rect(10, 5, 20, 10), "Lives = "+GameState.state.Lives, textStyle);
        //GUI.Label(new Rect(10, 25, 20, 10), "Score = "+GameState.state.Score, textStyle);
        if (GameState.state.GameOver){
            //PauseMenu.pauseMenu.GameOverMenu();
            GUI.Label(new Rect(Screen.width/3, Screen.height/2, 20, 10), "YOU SUCK", resultTextStyle);
        }else if(GameState.state.LevelComplete){
            //PauseMenu.pauseMenu.GameOverMenu();
            GUI.Label(new Rect(Screen.width/3, Screen.height/2, 20, 10), "U DA BESSSSSSSSSSS", resultTextStyle);
        }
        //GUI.DrawTexture(new Rect((xOffset, yOffset, textureWidth, textureHeight, textureToDraw, ScaleMode.ScaleToFit, true);
    }
    
    void FixedUpdate() {
		//Counts you as being on water if you're in there for 2 or more frames
        if(inWaterFrames >= 2){
			resetWaiter();
            audio.PlayOneShot(splash);
            audio.PlayOneShot(laughing);
			GameState.state.WaitererFell();
		}
    }
    // Check for any collisions
     void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Finish"){ //create a new waiter at the starting point
            //GameObject newWaiterer = (GameObject)Instantiate(waiterPrefab, transform.position, transform.rotation);
            GameObject deliveredFood = (GameObject)Instantiate(foodDelivered[Random.Range(0, foodDelivered.Count -1)], transform.position, transform.rotation);
            resetWaiter();
            GameState.state.Score += 100;
            GameState.state.CustomerServed();   
        }else if (other.gameObject.tag == "Obstacle"){
            resetWaiter();
            audio.PlayOneShot(crashWhistle);
            audio.PlayOneShot(crashDishes);
            audio.PlayOneShot(laughing);
            GameState.state.WaitererFell();
        }else if (other.gameObject.tag == "Platform" && !hopping){
			inWaterFrames = 0;
            transform.parent = other.transform;
            hopping = false;
            onPlatform = true;
        }else {
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
		transform.parent = null;
        hopping = false;
		inWaterFrames = 0;
        transform.position = startPoint.position;
        targetDisplacement = transform.position;
        lastHeightReached = transform.position.y;
    }
}
