using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static GameState state;
    public static int STARTING_LIVES = 5;
    public static int CUSTOMERS = 5;
    
    public AudioClip levelComplete;
    public AudioClip gameOver;
    public AudioClip happyCustomer;
    
    private bool _paused;
    private int _lives = STARTING_LIVES;
    private float _score = 0.00f;
    private int _customersServed = 0;
    private bool _gameOver = false;
    private bool _levelComplete = false;
    private int _level = 0;


	void Start () {  
        if(state != null) {
            Debug.LogError("There can never be two pause menus... Something went terribly wrong");
			Destroy(this.gameObject);
            return;
        }
        state = this;
        _paused = false;
        _levelComplete = false;
	}
    
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }

    public void OnGUI() { 
        if (_gameOver){
            PauseMenu.pauseMenu.GameOverMenu();
        }else if(_levelComplete){
            _level++;
            PauseMenu.pauseMenu.LevelCompleteMenu();
        }
    }
	void Update () {

	}
    
    public void PauseToggle(){
        _paused = !_paused;
    }
    
    public bool Paused
    {
        get { return _paused; }
        set { _paused = value; }
    }
    
    public bool GameOver
    {
        get { return _gameOver; }
        set { _gameOver = value; }
    }
    
    public bool LevelComplete
    {
        get { return _levelComplete; }
        set { _levelComplete = value; }
    }
    
    public float Score
    {
        get { return _score; }
        set { _score = value; }
    }
    
    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }
    
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }
    public void WaitererFell(){
        _lives--;
        if (_lives == 0){
            _gameOver = true;
            audio.PlayOneShot(gameOver);
        }
    }
    
    public void CustomerServed(){
        _customersServed++;
        if(_customersServed == CUSTOMERS){
            _score += TimeBar.timer.calculateScore ();
            audio.PlayOneShot(levelComplete);
            _levelComplete = true;
        }else{
            audio.PlayOneShot(happyCustomer);
        }
    }
    
    public void TimeOut(){
        audio.PlayOneShot(gameOver);
        _gameOver = true;
    }
    public void ResetGameState(){
        _customersServed = 0;
        _gameOver = false;
        _levelComplete = false;
        _lives = STARTING_LIVES;
        _paused = false;
        _score = 0.00f;
    }
}
