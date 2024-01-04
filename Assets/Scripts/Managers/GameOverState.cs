using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverState : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private LineRenderer limitLine;
    [SerializeField] private Transform emojiParent;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private LineRenderer dropLine;
    [SerializeField] private InterstetialController interstetialController;
    private AudioSource gameOverPop;


    [Header("Timer")]
    [SerializeField] private float durationThreshold;
    private float timer;
    private bool timerOn;
    

    // Start is called before the first frame update
    void Start()
    {
        gameOverPop = GetComponent<AudioSource>();
      GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void GameStateChangedCallback(GameState gameState){
        if(gameState == GameState.Game){
            //Debug.Log("Limit line should be enabled");
            limitLine.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
      if(GameManager.instance.IsGameState()){
        ManageGameOver();
      }
    }

    private void ManageGameOver(){
        if(timerOn){
            ManageTimer();
        }else{
            if(CheckEmojiAboveLine()){
                StartTimer();
            }
        }
    }


    private bool CheckEmojiAboveLine(){
        for(int i = 0;i<emojiParent.childCount; i++){
            if(!emojiParent.GetChild(i).GetComponent<Emoji>().HasCollided()){
                continue;
            }

            if(CheckEmojiAboveLine(emojiParent.GetChild(i))){
                return true;
            }
        }
        return false;
    }

    private bool CheckEmojiAboveLine(Transform Emoji){
        if(Emoji.position.y + Emoji.transform.localScale.y/2  > limitLine.transform.position.y){
            return true;
        }
        return false;
    }

    private void ManageTimer(){
        timer += Time.deltaTime;

        timerText.text = Mathf.FloorToInt(timer+1).ToString();

        if(!CheckEmojiAboveLine()){
            StopTimer();
            timerText.text = "";
        }

        if(timer>durationThreshold){
            GameOver();
            Destroy();
            timerText.text = "";
        }
        

    }

    private void Destroy(){
        InvokeRepeating(nameof(DestroyChild),0f,0.15f);
    }

    private void DestroyChild(){
        if(emojiParent.childCount == 0){
            CancelInvoke();
            interstetialController.ShowAd();
            GameManager.instance.SetGameOverState();
            //Debug.Log("still checking");
        }else{
            //Debug.Log(emojiParent.childCount);
            Destroy(emojiParent.GetChild(UnityEngine.Random.Range(0,emojiParent.childCount)).gameObject);
            gameOverPop.pitch = UnityEngine.Random.Range(0.8f,1.1f);
            gameOverPop.Play();
        }
       
    }



    private void GameOver(){
        StopTimer();
        dropLine.enabled = false;
        GameManager.instance.SetPostGameState();
        limitLine.enabled = false;
    }


    private void StartTimer(){
        timer = 0;
        timerOn = true;

    }
        

    private void StopTimer(){
        timerOn = false;
    }

}
