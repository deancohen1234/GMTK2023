using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public PlayableDirector director;

    public CanvasGroup mainMenu;
    public CanvasGroup songSelectMenu;
    public EndScreen endScreen;
    public ScoreTracker scoreTracker;

    public ShooterSequencer sequencer;

    public bool isGameRunning;

    [Header("Rankings")]
    public float dRankThreshold = 0.5f;
    public float cRankThreshold = 0.6f;
    public float bRankThreshold = 0.7f;
    public float aRankThreshold = 0.8f;
    public float sRankThreshold = 0.9f;
    public float sSRankThreshold = 0.95f;

    private int shotsHit;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.alpha = 1;
        songSelectMenu.alpha = 0;
        songSelectMenu.interactable = false;
        songSelectMenu.blocksRaycasts = false;

        endScreen.Hide();

        director.stopped += OnDirectorStopped;
    }

    public void StartLevel()
    {
        scoreTracker.score = 0;
        scoreTracker.UpdateScoreText();
        shotsHit = 0;

        songSelectMenu.alpha = 0;
        songSelectMenu.interactable = false;
        songSelectMenu.blocksRaycasts = false;

        director.Play();

        isGameRunning = true;
    }

    public void ShowSongSelect()
    {
        mainMenu.alpha = 0;
        songSelectMenu.alpha = 1;
        songSelectMenu.interactable = true;
        songSelectMenu.blocksRaycasts = true;
    }

    public void PlayTrack(ShooterSequence sequence)
    {
        sequencer.sequence = sequence;
        StartLevel();
        //sequencer.StartSequence();
    }

    private void OnDirectorStopped(PlayableDirector director)
    {
        StartShooter();
    }

    private void StartShooter()
    {
        sequencer.StartSequence();
    }

    public void ShowStartScreen()
    {
        mainMenu.alpha = 1;
        endScreen.Hide();

        isGameRunning = false;
    }

    public void ShowEndScreen(int totalShots)
    {
        float finalScore = (float)shotsHit / (float)totalShots;
        endScreen.Show(finalScore, GetRank(finalScore));
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void UpdateShotsHit()
    {
        shotsHit++;
    }

    private string GetRank(float percentage)
    {
        if (percentage >= sSRankThreshold)
        {
            return "SS-alacious";
        }
        else if (percentage >= sSRankThreshold)
        {
            return "S-ick";
        }
        else if (percentage >= aRankThreshold)
        {
            return "A-wesome";
        }
        else if (percentage >= bRankThreshold)
        {
            return "B-y Goly";
        }
        else if (percentage >= cRankThreshold)
        {
            return "C-ould Improve";
        }
        else if (percentage >= dRankThreshold)
        {
            return "D-eserved More";
        }
        else
        {
            return "E-hhh";
        }
    }

}
