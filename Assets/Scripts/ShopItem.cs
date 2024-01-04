using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ShopItem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private GameObject costContainer;
    [SerializeField] private GameObject ownedContainer;

    public void Setup(string title, int cost, Sprite image, bool isOwned){
        titleText.text = title;
        costText.text = cost.ToString();
        icon.sprite = image;
        costContainer.SetActive(!isOwned);
        ownedContainer.SetActive(isOwned);
    }

    public void SetOwned(){
        costContainer.SetActive(false);
        ownedContainer.SetActive(true);
    }

    public Button GetButton(){
        return button;
    }
}
