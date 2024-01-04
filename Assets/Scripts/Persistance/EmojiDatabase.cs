using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiDatabase : MonoBehaviour
{
   public static EmojiDatabase _i;


    [SerializeField] private Sprite[] emojiAssets; 

   // [SerializeField] private EmojiSaveData[] emojiData;


    void Awake(){
        if(_i == null){
            _i = this;
        }else{
            Destroy(gameObject);
        }
    }

    void Start(){
        //SaveDataManager.GetAllEmojis(out List<EmojiSaveData> allEmojis);


        //emojiData = allEmojis.ToArray();


    }


    public Sprite GetEmoji(int index){
        return emojiAssets[index];
    } 

    // public EmojiSaveData[] GetEmojis(){
    //     return emojiData;
    // }




}
