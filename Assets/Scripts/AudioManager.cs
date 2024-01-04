using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource bubbleClip;
    [SerializeField] private AudioSource tapClip;
    [SerializeField] private AudioSource mainSound;
    [SerializeField] private GameObject[] crosses;


    // Start is called before the first frame update
    void Awake()
    {
        MergeManager.onMergeProcessed += PlayAudioClip;
        ActiveSpriteManger.onTapped += TapSound;
    }


    private void PlayAudioClip(EmojiType type, Vector2 pos){


        bubbleClip.pitch = UnityEngine.Random.Range(0.8f, 1.1f);

        bubbleClip.Play();
    }

    public void ChangeMainSound(){
        mainSound.mute = !mainSound.mute;
        foreach(GameObject x in crosses){
            x.SetActive(mainSound.mute);
        }

    }

    public void TapSound(){
        tapClip.Play();
    }

    
}
