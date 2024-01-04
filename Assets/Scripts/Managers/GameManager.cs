using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   

    public static GameManager instance;
    
    [Header("Actions")]
    public static Action<GameState> onGameStateChanged;

    [Header("Settings")]
    private GameState gameState;



    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 45;

        SetMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetGame(){
        SetTheGameState(GameState.Game);
    }

    private void SetMenu(){
        SetTheGameState(GameState.Menu);
    }

    private void SetPostGame(){
        SetTheGameState(GameState.PostGame);
    }

    private void SetGameOver(){
        SetTheGameState(GameState.GameOver);
    }

    private void SetTheGameState(GameState gameState){
        this.gameState = gameState;
        Debug.Log("game state changed " + gameState.ToString());
        onGameStateChanged?.Invoke(gameState);
    }


    public GameState GetGameState(){
        return gameState;
    }


    public bool IsGameState(){
        return gameState == GameState.Game;
    }


    public void SetGameState(){
        SetGame();
    }

    public void SetPostGameState(){
        SetPostGame();
    }

    public void SetMenuState(){
        SetMenu();
    }

    public void SetGameOverState(){
        SetGameOver();
    }





}
