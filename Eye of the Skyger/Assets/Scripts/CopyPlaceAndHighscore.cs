using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CopyPlaceAndHighscore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI place;
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] float restartTimer = 7;
    float timer;

    
    // Start is called before the first frame update
    void Start()
    {
        place.text = ScoreManager.Instance.place.ToString();
        highscore.text = ScoreManager.Instance.highscore.ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= restartTimer)
        {
            timer = 0;
            SceneManager.LoadScene(0);
        }
    }
}
