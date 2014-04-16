using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static GameState state;
    public static int STARTING_LIVES = 5;
    public static int CUSTOMERS = 5;
    
    private bool _paused;
    private int _lives = STARTING_LIVES;
    private int _score = 0;
    private int _customersServed = 0;
    private bool _gameOver = false;
    private bool _levelComplete = false;


	void Start () {  
        if(state != null) {
            Debug.LogError("There can never be two pause menus... Something went terribly wrong");
            return;
        }
        state = this;
        _paused = false;
	}

    public void OnGUI() { 
        if (_gameOver){
            PauseMenu.pauseMenu.GameOverMenu();
        }else if(_levelComplete){
            PauseMenu.pauseMenu.GameOverMenu();
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
    
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    
    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }
    
    public void WaitererFell(){
        _lives--;
        if (_lives == 0){
            _gameOver = true;
        }
    }
    
    public void CustomerServed(){
        _customersServed++;
        if(_customersServed == CUSTOMERS){
            _levelComplete = true;
        }
    }
}
