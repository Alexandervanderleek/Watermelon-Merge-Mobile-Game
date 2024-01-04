using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmojiButton : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI slotText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Button button;


    public void Setup(Sprite icon, string slotText, string nameText){
        iconImage.sprite = icon;
        this.slotText.text  = slotText;
        this.nameText.text = nameText;
    }

    public void UpdateElement(Sprite icon, string nameText){
        iconImage.sprite = icon;
        this.nameText.text = nameText;
    }

    public Button GetButton(){
        return button;
    }
}
