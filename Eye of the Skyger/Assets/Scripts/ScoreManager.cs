using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MultiplierText;

    float score;
    float multiplier = 1;

    [SerializeField] float increaseSteps = 0.2f;

    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake() {
        if (instance != null)
            return;
        instance = this;
    }

    void Update()
    {
        score += Time.deltaTime * multiplier * 100;
        ScoreText.text = score.ToString("###,###,###");
    }

    public void IncreaseMultiplier()
    {
        multiplier += increaseSteps;
        MultiplierText.text = multiplier.ToString("##.##");
    }

    public void ResetMultiplier()
    {
        multiplier = 0.5f;
    }
}
