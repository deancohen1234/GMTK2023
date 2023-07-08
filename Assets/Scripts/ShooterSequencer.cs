using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSequencer : MonoBehaviour
{
    public ShooterSequence sequence;
    public Shooter shooter;

    private AudioSource source;
    private int sequenceIndex = 0;

    private bool isPlaying = false;

    #region MonoBehavior
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!shooter.isActive)
            {
                StartSequence();
            }
            else
            {
                StopSequence();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //finish song
            source.time = sequence.song.length - 5f;

        }

        //only evaluate when the sequence is running
        if (isPlaying)
        {
            EvaluateMusicTrackTime();
        }
    }
    #endregion

    public void StartSequence()
    {
        shooter.Activate();
        source.clip = sequence.song;
        source.time = 0;

        UpdateTimeFrame();

        source.Play();

        isPlaying = true;
    }

    public void StopSequence()
    {
        shooter.DeActivate();

        source.Stop();

        isPlaying = false;

        sequenceIndex = 0;

        //send shots fired so we can display a score
        GameManager.instance.ShowEndScreen(shooter.GetShotsFired());
    }

    private float GetShotDelay(ShooterTimeFrame.Subdivision subdivision)
    {
        switch(subdivision)
        {
            case ShooterTimeFrame.Subdivision.Whole:
                return 240f / sequence.songBPM;
            case ShooterTimeFrame.Subdivision.Half:
                return 120f / sequence.songBPM;
            case ShooterTimeFrame.Subdivision.Quater:
                return 60f / sequence.songBPM;
            case ShooterTimeFrame.Subdivision.Eighth:
                return 30f / sequence.songBPM;
            case ShooterTimeFrame.Subdivision.Sixteenth:
                return 15f / sequence.songBPM;
            default:
                Debug.LogError("BADDDD");
                return -1f;
        }
    }

    private void EvaluateMusicTrackTime()
    {
        if (sequenceIndex >= sequence.shooterTimeFrames.Length)
        {
            return;
        }

        //check if song ends
        if (!source.isPlaying)
        {
            StopSequence();
            return;
        }

        //passed this time frame
        if (source.time >= sequence.shooterTimeFrames[sequenceIndex].endTime)
        {
            MoveToNextTimeFrame();
        }
    }

    private void MoveToNextTimeFrame()
    {
        sequenceIndex++;

        //apply to shooter

        UpdateTimeFrame();
    }

    private void UpdateTimeFrame()
    {
        shooter.shooterType = sequence.shooterTimeFrames[sequenceIndex].shooterType;
        shooter.shotDelay = GetShotDelay(sequence.shooterTimeFrames[sequenceIndex].subdivision);
        shooter.projectileSpeed = sequence.shooterTimeFrames[sequenceIndex].projectileSpeed;
        shooter.frequency = sequence.shooterTimeFrames[sequenceIndex].waveFrequency;
        shooter.angleDamper = sequence.shooterTimeFrames[sequenceIndex].angleDampen;

        shooter.SetColor(sequence.shooterTimeFrames[sequenceIndex].color);
    }
}
