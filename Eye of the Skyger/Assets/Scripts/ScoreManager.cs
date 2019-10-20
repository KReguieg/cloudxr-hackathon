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
    [SerializeField] List<AudioClip> clips;
    [SerializeField] Transform ship;
    [SerializeField] GameObject WorldMultiplier;
    AudioSource source;

    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }
    public float Score
    {
        get
        {
            return score;
        }
    }

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.singleton.gameOver)
        {
            score += Time.deltaTime * multiplier * 100;
            ScoreText.text = score.ToString("###,###,###");
        }
    }
    int oldMulti = 1;
    public void IncreaseMultiplier()
    {
        multiplier += increaseSteps;
        MultiplierText.text = multiplier.ToString("##.##");
        if ((int)multiplier > oldMulti)
        {
            GameObject worldMul = Instantiate(WorldMultiplier, ship);
            worldMul.GetComponentInChildren<TextMeshProUGUI>().text = multiplier.ToString("x 00");
            Destroy(worldMul,2);
            oldMulti = (int)multiplier;
            int index = Mathf.Clamp(oldMulti, 0, clips.Count - 1);
            source.PlayOneShot(clips[index]);
        }
    }

    public void ResetMultiplier()
    {
        multiplier = 0.5f;
        oldMulti = (int)multiplier;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("InC"))
        {
            IncreaseMultiplier();
        }
    }
}
