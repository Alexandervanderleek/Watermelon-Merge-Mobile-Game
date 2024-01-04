using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveDataManager
{    

   
    [Serializable]
    public class ActiveEmojiListSaveData{
        public List<EmojiSaveData> entries = new List<EmojiSaveData>();

        public ActiveEmojiListSaveData(List<EmojiSaveData> entries){
            this.entries = entries;
        }

    }


    public static bool GetAllEmojis(out List<EmojiSaveData> data){
        if(File.Exists(GetAllEmojiDirectory())){
            
            Debug.Log("Loading active file");

            string DataJson = File.ReadAllText(GetAllEmojiDirectory());

            ActiveEmojiListSaveData loadedData = JsonUtility.FromJson<ActiveEmojiListSaveData>(DataJson);

            data = loadedData.entries;

            //Debug.Log(data[0].ToString());

            return true;
        }

        Debug.Log("New file required");


        

        //data = new List<EmojiSaveData>( EmojiDatabase._i.GetEmojis());

        //Load text from a JSON file (Assets/Resources/Text/jsonFile01.json)
        var jsonTextFile = Resources.Load<TextAsset>("GameData/EmojiData");
        
        data = JsonUtility.FromJson<ActiveEmojiListSaveData>(jsonTextFile.text).entries;

        SaveAllEmojis(data);


        return false;
    }



    public static bool GetActiveEmojiList(out List<EmojiSaveData> data){
        if(File.Exists(GetActiveSaveDirectory())){
            
            Debug.Log("Loading active file");

            string DataJson = File.ReadAllText(GetActiveSaveDirectory());

            ActiveEmojiListSaveData loadedData = JsonUtility.FromJson<ActiveEmojiListSaveData>(DataJson);

            data = loadedData.entries;

            //Debug.Log(data[0].ToString());

            return true;
        }

        Debug.Log("New file required");


        var jsonTextFile = Resources.Load<TextAsset>("GameData/defaults");
        
        data = JsonUtility.FromJson<ActiveEmojiListSaveData>(jsonTextFile.text).entries;

        SaveActiveEmojiList(data);


        return false;
    }


    public static void SaveActiveEmojiList(List<EmojiSaveData> emojis){
        ActiveEmojiListSaveData data = new ActiveEmojiListSaveData(emojis);
        string dataJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetActiveSaveDirectory(), dataJson);
    }


    public static void SaveAllEmojis(List<EmojiSaveData> emojis){
        ActiveEmojiListSaveData data = new ActiveEmojiListSaveData(emojis);
        string dataJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetAllEmojiDirectory(), dataJson);
    }




    public static string GetSaveDirectory(){
        return Application.persistentDataPath;
    }

    static string GetActiveEmojiFileName(){
        return "active.json";
    }

    static string GetAllEmojis(){
        return "all.json";
    }

    static string GetAllEmojiDirectory(){
        return GetSaveDirectory() + "/" + GetAllEmojis();
    }

    static string GetActiveSaveDirectory(){
        return GetSaveDirectory() + "/" + GetActiveEmojiFileName();
    }
    


}
