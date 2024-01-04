using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EmojiSaveData 
{
    public string emojiName;
        public int emojiImage;
        public bool available;
        public int cost;
        public bool isActive;

        public EmojiSaveData(string emojiName, int emojiImage, bool available, int cost, bool isActive ){
            this.cost = cost;
            this.emojiImage = emojiImage;
            this.available = available;
            this.emojiName = emojiName;
            this.isActive = isActive;
        }


       public void Update(EmojiSaveData newEmoji){
            cost = newEmoji.cost;
            emojiImage = newEmoji.emojiImage;
            available = newEmoji.available;
            emojiName = newEmoji.emojiName;
            isActive = newEmoji.isActive;
       } 
}
