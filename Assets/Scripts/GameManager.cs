using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public CanvasGroup endScreen;

    public ShooterSequencer sequencer;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.alpha = 1;
        //endScreen.alpha = 0;
    }

    public void StartLevel()
    {
        mainMenu.alpha = 0;
        sequencer.StartSequence();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
