using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{

    [Header("Variables")]
    Emoji lastSender;

    [Header("Actions")]
    public static Action<EmojiType, Vector2> onMergeProcessed;

    void Awake(){
        Emoji.onCollisionOtherEmoji += CollisionBetweenEmojiCallback;
    }

    void OnDestroy(){
        Emoji.onCollisionOtherEmoji -= CollisionBetweenEmojiCallback;
    }

    void CollisionBetweenEmojiCallback(Emoji sender, Emoji other){
        if(lastSender != null){
            return;
        }

        lastSender = sender;

        ProcessMerge(sender, other);
    }

    public void ProcessMerge(Emoji sender, Emoji other){
      EmojiType mergeType = sender.GetEmojiType() + 1;

      // Need to design edge case for final.

      Vector2 spawnPos = ( sender.transform.position + other.transform.position ) / 2;

      sender.Merge();
      other.Merge();

      StartCoroutine(ResetLastSender());

      onMergeProcessed?.Invoke(mergeType, spawnPos);

    
    }

    IEnumerator ResetLastSender(){
        
        yield return new WaitForEndOfFrame();

        lastSender = null;
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
