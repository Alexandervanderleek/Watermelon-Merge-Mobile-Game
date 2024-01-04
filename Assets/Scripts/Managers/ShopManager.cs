using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{



    [Header("Data")]
    [SerializeField] private EmojiSaveData[] currentItems;
    [SerializeField] private ShopItem[] shopItems;
    

    [Header("Elements")]
    [SerializeField] private Transform shopItemsParent;
    [SerializeField] private ShopItem shopItem;

    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private Button button;
    [SerializeField] private AudioSource errorNoise;


    void Start(){
        SaveDataManager.GetAllEmojis(out List<EmojiSaveData> data);

        currentItems = data.ToArray();



        SpawnShopButtons(currentItems);

    }



    private void SpawnShopButtons(EmojiSaveData[] data){

        shopItems = new ShopItem[data.Length - 11];

        for(int i = 11; i<data.Length;i++){
            //Debug.Log(i);
            if(data[i].cost > 0){
                SpawnButton(data[i], i);

            }
            //Debug.Log(i);
        }
    }

    private void SpawnButton(EmojiSaveData dataIn, int index){
        ShopItem instance = Instantiate(shopItem, shopItemsParent);


        instance.Setup(dataIn.emojiName, dataIn.cost, EmojiDatabase._i.GetEmoji( dataIn.emojiImage), dataIn.available);

        shopItems[index-11] = instance;

        instance.GetButton().onClick.AddListener(()=> PurchaseButton(index));
        
    }

    private void PurchaseButton(int index){
        //Debug.Log(index);

        //Debug.Log("purchase" + index);

        button.onClick.RemoveAllListeners();
        

        if(currentItems[index].available){
           // Debug.Log("Owned");
            errorNoise.Play();
            return;
        }

        if(currentItems[index].cost > CoinManager.instance.GetCoins()){
            //Debug.Log("Not enough coins");
            errorNoise.Play();
            return;
        }

        confirmPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"ARE YOU SURE YOU WANT TO BUY {currentItems[index].emojiName} FOR {currentItems[index].cost} COINS";

        //Debug.Log("confirm purchase");
        confirmPanel.SetActive(true);

        button.onClick.AddListener(()=>ConfirmPurchase(index));



    
    }

    private void ConfirmPurchase(int index){
        
        //Debug.Log("confirm purchase "+index);

        CoinManager.instance.UseCoins(currentItems[index].cost);

        currentItems[index].available = true;

        shopItems[index-11].SetOwned();

        SaveDataManager.SaveAllEmojis(currentItems.ToList());

        ActiveSpriteManger.instance.AddNew(index);

        confirmPanel.SetActive(false);

    }


    public void DisableConfirmPanel(){
        button.onClick.RemoveAllListeners();
        confirmPanel.SetActive(false);
    }


}
