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

    [SerializeField] float increaseSteps = 0.5f;
    [SerializeField] List<AudioClip> clips;
    [SerializeField] PlayerController ship;
    [SerializeField] GameObject WorldMultiplier;
    AudioSource source;
    public int ScoreSaverCount;
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

    private void Start()
    {
        GameManager.singleton.CalcScore.AddListener(GameEnd);
    }
    public int highscore;
    public int Place;
    List<int> scores;
    void GameEnd()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highscore)
        {
            highscore = (int)score;
            PlayerPrefs.SetInt("HighScore", (int)score);
        }

        for (int i = 0; i < ScoreSaverCount - 1; i++)
        {
            scores.Add(PlayerPrefs.GetInt((i).ToString()));
        }

        //scores.Sort();
        PlayerPrefs.SetInt("0", (int)score);
        Place = 1;
        for (int i = 1; i < ScoreSaverCount; i++)
        {
            if ((int)score >= scores[i - 1])
                Place++;
            PlayerPrefs.SetInt((i).ToString(), scores[i]);

        }

        PlayerPrefs.Save();
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
        IncreaseMultiplier(0);
    }
    public void IncreaseMultiplier(int amount = 0)
    {
        multiplier += increaseSteps;
        multiplier += amount;

        MultiplierText.text = multiplier.ToString("##.##");
        if ((int)multiplier > oldMulti)
        {
            GameObject worldMul = Instantiate(WorldMultiplier, ship.transform);
            worldMul.GetComponentInChildren<TextMeshProUGUI>().text = multiplier.ToString("x 00");
            Destroy(worldMul, 2);
            oldMulti = (int)multiplier;
            int index = Mathf.Clamp(oldMulti, 0, clips.Count - 1);
            source.PlayOneShot(clips[index]);
            ship.SetMultiplier(multiplier);
        }
    }

    public void ResetMultiplier()
    {
        multiplier = 0.5f;
        oldMulti = (int)multiplier;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (GUILayout.Button("InC"))
        {
            IncreaseMultiplier();
        }
    }
#endif
}
