using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject ActtiveEmojiPanel;
    [SerializeField] private GameObject StorePanel;
    

    void Awake()
    {
        GameManager.onGameStateChanged += StateChangedCallback;     
    }

    private void StateChangedCallback(GameState gameState){
        switch(gameState){
            case GameState.Menu:{
                SetMenu();
                break;
            }
            case GameState.Game:{
                SetGame();
                break;
            }
            case GameState.PostGame:{
                SetPostGame();
                break;
            }

            case GameState.GameOver:{
                SetGameOver();
                break;
            }
        }
    }

    private void SetMenu(){
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void SetGame(){
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    private void SetPostGame(){
        gamePanel.SetActive(false);
    
    }

    private void SetGameOver(){
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void EmojiActivePanel(){
        ActtiveEmojiPanel.SetActive(true);
    }

    public void DisableEmojiActivePanle(){
        ActtiveEmojiPanel.SetActive(false);
    }

     public void StorePanelEnable(){
        StorePanel.SetActive(true);
    }

    public void DisableStorePanel(){
        StorePanel.SetActive(false);
    }

     

    public void PlayButtonCallback(){
        GameManager.instance.SetGameState();
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
