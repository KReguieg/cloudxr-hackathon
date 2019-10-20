﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject[] onStartActivatedObjects;
    [SerializeField] GameObject MainMenuItem;
    private void Start()
    {
        foreach (var item in onStartActivatedObjects)
        {
            item.SetActive(false);
        }
    }

    public void StartGame()
    {
        foreach (var item in onStartActivatedObjects)
        {
            item.SetActive(true);
        }
        MainMenuItem.SetActive(false);
        GameManager.singleton.gameStarted = true;
    }
}