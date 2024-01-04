using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ActiveItem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    public void Setup(string title, Sprite image){
        titleText.text = title;
        icon.sprite = image;
    }

    public void UpdateActiveItem(string newTitle, Sprite newImage){
        titleText.text = newTitle;
        icon.sprite = newImage;
    }


    public Button GetButton(){
        return button;
    }
}
