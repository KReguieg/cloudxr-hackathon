using System.Collections;
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
    bool go = false;
    public void StartGame()
    {
        foreach (var item in onStartActivatedObjects)
        {
            item.SetActive(true);
        }
        go = true;
        //M//ainMenuItem.SetActive(false);
        GameManager.singleton.gameStarted = true;
    }

    private void Update()
    {
        if (go)
            MainMenuItem.transform.position += Vector3.forward * -30 * Time.deltaTime;
    }
}
