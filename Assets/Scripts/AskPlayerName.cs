﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class AskPlayerName : MonoBehaviour
{
    public string playername1;
    public string saveName;
    public string playername2;
    public string saveName2;
  

    public Text inputText2;
    public Text inputText;
   
    void Start()
    {
        
    }
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Update()
    {
        playername1 = PlayerPrefs.GetString("name", "none");
        playername2 = PlayerPrefs.GetString("name", "none");

    }
    public void SetName1()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);

       
    }
    public void SetName2()
    {

        saveName2 = inputText2.text;
        PlayerPrefs.SetString("name", saveName2);

    }

}
