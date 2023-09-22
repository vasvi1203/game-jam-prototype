using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CategoryBtnScript : MonoBehaviour
{
    [SerializeField] private Text categoryTitleText;
    //[SerializeField] private Text scoreText;
    [SerializeField] private Button btn;

    public Button Btn { get => btn; }

    public void SetButton(string title)
    {
        categoryTitleText.text = title;
        // categoryTitleText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        //scoreText.text = PlayerPrefs.GetInt(title, 0) + "/" + totalQuestion; //we get the score save for this category
    }

}
