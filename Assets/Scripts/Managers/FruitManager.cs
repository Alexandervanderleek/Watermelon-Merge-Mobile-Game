using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Image = UnityEngine.UI.Image;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Emoji[] baseEmojis;
    [SerializeField] private Emoji[] possibleEmojis;
    [SerializeField] private Transform emojiParent;
    [SerializeField] private LineRenderer dropLine;
    [SerializeField] private Image nextIcon;
    private Emoji currentEmoji;


    [Header("Settings")]
    [Range(0.2f,1f)]
    [SerializeField] private float Height;
    [SerializeField] private float delay;
    private bool canManage;
    

    [Header("Next Fruit Decider")]
    private int nextFruitIndex = 0;


    void Awake(){
        InputManager.fingerDownBroadCast += FingerDownCallback;
        InputManager.fingerUpBroadCast += FingerUpCallback;
        InputManager.fingerDragBroadCast += FingerDrapCallback;

        MergeManager.onMergeProcessed += MergeProcessedCallback;
    
    }

    void OnDestroy(){
        InputManager.fingerDownBroadCast -= FingerDownCallback;
        InputManager.fingerUpBroadCast -= FingerUpCallback;
        InputManager.fingerDragBroadCast -= FingerDrapCallback;

        MergeManager.onMergeProcessed += MergeProcessedCallback;


    }

    void MergeProcessedCallback(EmojiType type, Vector2 position){
        for(int i = 0; i<baseEmojis.Length;i++){
            if(baseEmojis[i].GetEmojiType() == type){
                SpawnMergedFruit(baseEmojis[i], position);
                break;
            }
        }
    }

    void SpawnMergedFruit(Emoji emoji, Vector2 position){
        Emoji emojiInstance = Instantiate(emoji, position, Quaternion.identity,emojiParent);
        emojiInstance.EnablePhysics();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstRandom());
        //GenerateRandomIndex();
        dropLine.enabled = false;
        canManage = true;
    }

    IEnumerator FirstRandom(){
        yield return new WaitForEndOfFrame();
        GenerateRandomIndex();
    }

    // Update is called once per frame
    void Update()
    {

        if(!GameManager.instance.IsGameState()){
            return;
        }

        if(canManage){
            ManageInput();
        }
    }

    private void GenerateRandomIndex(){
        nextFruitIndex = UnityEngine.Random.Range(0, possibleEmojis.Length);
        nextIcon.sprite = ActiveSpriteManger.instance.GetActiveSpriteAt(possibleEmojis[nextFruitIndex].GetMytype());
    }


    private void ManageInput(){
        if(Touch.activeTouches.Count > 0){
            if(Touch.activeTouches[0].finger.index == 0){
                Touch myTouch = Touch.activeTouches[0];

                Vector2 touchPos = myTouch.screenPosition;


                if(Touch.activeTouches[0].phase == TouchPhase.Began){
                    FingerDownCallback(touchPos);
                }

                if(Touch.activeTouches[0].phase == TouchPhase.Moved){
                    FingerDrapCallback(touchPos);
                }

                if(Touch.activeTouches[0].phase == TouchPhase.Ended){
                    FingerUpCallback();
                }




            }
        }
    }


    private void FingerDownCallback(Vector2 position){
        Vector2 spawnPosition = GetSpawnPosition(position);
        
        Emoji toSpawn = possibleEmojis[nextFruitIndex];

        currentEmoji = Instantiate(toSpawn, spawnPosition, Quaternion.identity, emojiParent);

        dropLine.enabled = true;

        GenerateRandomIndex();

        

    }

    private void FingerDrapCallback(Vector2 position){
        
        Vector2 spawnPosition = GetSpawnPosition(position); 

        currentEmoji?.MoveTo(spawnPosition);
   
    }


    private void FingerUpCallback(){

        if(currentEmoji != null){
            currentEmoji.EnablePhysics();
        }

        dropLine.enabled = false;

        currentEmoji = null;

        canManage = false;
        StartControlTimer();

    }

    private void StartControlTimer(){
        Invoke("StopController",delay);
    }

    private void StopController(){
        canManage = true;
    }


    private Vector2 GetSpawnPosition(Vector2 position){
        Vector2 screenTapped  = Camera.main.ScreenToViewportPoint(position);
        
        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(screenTapped.x,Height));

         dropLine.SetPosition(0, spawnPosition);
         dropLine.SetPosition(1, spawnPosition + Vector2.down * 15);
        
        return spawnPosition;
    }

}
