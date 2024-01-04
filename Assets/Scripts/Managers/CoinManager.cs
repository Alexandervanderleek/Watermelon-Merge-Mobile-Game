using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{

    private int coins;

    private int coinsInGame;

    public static CoinManager instance;

    //public static Action<int> onCoinUpdate;

    public static Action<int> onCoinInGameUpdate;

    [SerializeField] private TextMeshProUGUI[] textElements; 

    [SerializeField] private TextMeshProUGUI inGameText;

    [SerializeField] private TextMeshProUGUI addItionText;

    [SerializeField] private AudioSource ching;



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
        MergeManager.onMergeProcessed += AddCoinsEmoji;

        GameManager.onGameStateChanged += GameStateChangedCallback;

        coinsInGame = 0;

        LoadData();

        
    }

    void OnDestroy(){
        MergeManager.onMergeProcessed -= AddCoinsEmoji;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GameStateChangedCallback(GameState state){
        switch(state){
            case GameState.Game:{
                ResetInGame();
                break;
            }

            case GameState.GameOver:{
                UpdateCoins(coinsInGame);
                addItionText.text = "+ "+ coinsInGame.ToString();
                //ResetInGame();
                break;
            }

            default:{
                break;
            }
        }
    }

    private void LoadData(){
        //Debug.Log("loading coins");
        coins = PlayerPrefs.GetInt("coins",0);
        //Debug.Log("coins" + coins);

        

        //onCoinUpdate?.Invoke(coins);
        UpdateTextElements();

    }

    private void SaveData(){
        PlayerPrefs.SetInt("coins",coins);
    }


    private void UpdateCoins(int amount){
        coins += amount;
        //onCoinUpdate?.Invoke(coins);
        UpdateTextElements();

        SaveData();
    }

    public void UseCoins(int amount){
        coins -= amount;
        //onCoinUpdate?.Invoke(coins);
        UpdateTextElements();

        SaveData();
    }

    public int GetCoins(){
        return coins;
    }

    private void UpdateTextElements(){
        for(int i = 0; i<textElements.Length;i++ ){
            textElements[i].text = coins.ToString();
        }
    }

    public void RewardCoins(){
        UpdateCoins(coinsInGame);
        addItionText.text = "+ "+ (coinsInGame*2).ToString();
        ching.Play();


    }

    public void AddCoinsEmoji(EmojiType type, Vector2 pos){
        if(GameManager.instance.IsGameState()){
            coinsInGame +=  (int)type/2; 
            onCoinInGameUpdate?.Invoke(coinsInGame);
            inGameText.text = coinsInGame.ToString();
        }
        
    }

    public void ResetInGame(){

        addItionText.text = "+ "+ coinsInGame.ToString();

        coinsInGame = 0;
        inGameText.text = coinsInGame.ToString();

    }


}
