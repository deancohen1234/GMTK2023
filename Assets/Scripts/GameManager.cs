using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public PlayableDirector director;

    public CanvasGroup mainMenu;
    public EndScreen endScreen;
    public ScoreTracker scoreTracker;

    public ShooterSequencer sequencer;

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
        endScreen.Hide();

        director.stopped += OnDirectorStopped;
    }

    public void StartLevel()
    {
        scoreTracker.score = 0;
        scoreTracker.UpdateScoreText();
        shotsHit = 0;
        mainMenu.alpha = 0;
        director.Play();
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
    }

    public void ShowEndScreen(int totalShots)
    {
        float finalScore = (float)shotsHit / (float)totalShots;
        endScreen.Show(finalScore, "A");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void UpdateShotsHit(int _showsHit)
    {
        shotsHit = _showsHit;
    }

}
