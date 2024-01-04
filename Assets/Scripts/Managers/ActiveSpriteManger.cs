using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveSpriteManger : MonoBehaviour
{
    public static ActiveSpriteManger instance;

    [Header("Data")]
     public EmojiSaveData[] activeEmojis;
     public EmojiSaveData[] allEmojisAvailable;
     private EmojiButton[] slotButtons;
     private ActiveItem[] activeButton;
    

    private int activeEditSlot = -1;
    private int activeButtonsCounter = 0;

    public static Action onTapped;

    //[SerializeField] private 

    [Header("Elements")]
    [SerializeField] private Transform ActiveItemsParent;
    [SerializeField] private Transform AvailItemParent;
    [SerializeField] private EmojiButton emojiButton;
    [SerializeField] private ActiveItem activeItem;
    [SerializeField] private GameObject activePanel;
    [SerializeField] private GameObject noneAvailable;



    void Awake(){
       
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    
    }
    

    void Start()
    {

        slotButtons = new EmojiButton[11];
        activeButton = new ActiveItem[61];

        SaveDataManager.GetActiveEmojiList(out List<EmojiSaveData> savedEmojis);        
        
        activeEmojis = savedEmojis.ToArray();

        SpawnEmojiButtons();

        SpawnAvailableButtons();


    }


    public EmojiSaveData[] GetActiveEmojis(){
        return activeEmojis;
    }


    void Update()
    {
        
    }

    public Sprite GetActiveSpriteAt(int index){
         return EmojiDatabase._i.GetEmoji(activeEmojis[index].emojiImage);
    }

    public void SpawnEmojiButtons(){
        for(int i = 0; i<activeEmojis.Length;i++){
            SpawnButton(i);
        }
    }

    public void SpawnButton(int index){
        EmojiButton emojiButtonInstance = Instantiate(emojiButton, ActiveItemsParent);


        string slotText = "Slot "+index;


        emojiButtonInstance.Setup(EmojiDatabase._i.GetEmoji(activeEmojis[index].emojiImage), slotText , activeEmojis[index].emojiName);

        emojiButtonInstance.GetButton().onClick.AddListener(()=>AlterSlot(index));
        emojiButtonInstance.GetButton().onClick.AddListener(()=> onTapped?.Invoke() );

        slotButtons[index] = emojiButtonInstance;


    }

    private void AlterSlot(int index){
        ShowActivePanel(index);
    }

    private void SpawnAvailableButtons(){

        //Debug.Log("spawn avail buttons");

        SaveDataManager.GetAllEmojis(out List<EmojiSaveData> data );

        allEmojisAvailable =  data.ToArray();

        

        for(int i = 0; i<allEmojisAvailable.Length;i++){

            if(allEmojisAvailable[i].available == true && allEmojisAvailable[i].isActive == false ){
                SpawnAvailEmoji(i);
            }
        }

        if(AvailItemParent.transform.childCount == 0){
            noneAvailable.SetActive(true);
        } 

    }


    //SpawnAvailCalled  via shop manager

    private void SpawnAvailEmoji(int index){
        ActiveItem availButtonInstance = Instantiate(activeItem, AvailItemParent);



        availButtonInstance.Setup(allEmojisAvailable[index].emojiName, EmojiDatabase._i.GetEmoji(allEmojisAvailable[index].emojiImage));

        int currentValue = activeButtonsCounter;

        //Debug.Log(index.ToString());

        availButtonInstance.GetButton().onClick.AddListener(()=> SwapActive(index, currentValue));
        availButtonInstance.GetButton().onClick.AddListener(()=> onTapped?.Invoke());
        
        activeButton[activeButtonsCounter] = availButtonInstance;

        activeButtonsCounter++;
    }


    private void SwapActive(int index, int activeButtonPos){

        //Debug.Log("Swapping at index " + index);
        //Debug.Log(activeButtonPos);

        allEmojisAvailable[index].isActive = true;


        for(int i = 0; i< allEmojisAvailable.Length; i++){

            

            if(allEmojisAvailable[i].emojiName == activeEmojis[activeEditSlot].emojiName){
                
                    //Debug.Log(i);

                allEmojisAvailable[i].isActive = false;
                activeButton[activeButtonPos].Setup(allEmojisAvailable[i].emojiName,EmojiDatabase._i.GetEmoji(allEmojisAvailable[i].emojiImage));
                activeButton[activeButtonPos].GetButton().onClick.RemoveAllListeners();
                activeButton[activeButtonPos].GetButton().onClick.AddListener(()=>SwapActive(i,activeButtonPos));
                break;
            }
        }


        activeEmojis[activeEditSlot].Update(allEmojisAvailable[index]);

        slotButtons[activeEditSlot].UpdateElement(EmojiDatabase._i.GetEmoji(activeEmojis[activeEditSlot].emojiImage), activeEmojis[activeEditSlot].emojiName);


        
        



        SaveDataManager.SaveAllEmojis(allEmojisAvailable.ToList());
        SaveDataManager.SaveActiveEmojiList(activeEmojis.ToList());


        activePanel.SetActive(false);

    }


    public void AddNew(int index){

        //Debug.Log("add new");


        noneAvailable.SetActive(false);
        allEmojisAvailable[index].available = true;
        //fix
        SpawnAvailEmoji(index);
            
        
    }

    private void ShowActivePanel(int index){
        activeEditSlot = index;
        activePanel.SetActive(true);
    }

    public void HideActivePanel(){
        activePanel.SetActive(false);
    }


}
