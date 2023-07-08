using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSequencer : MonoBehaviour
{
    public ShooterSequence sequence;
    public Shooter shooter;
    public AudioClip track;

    private AudioSource source;
    private int sequenceIndex = 0;

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

        EvaluateMusicTrackTime();
    }

    private void StartSequence()
    {
        shooter.Activate();
        source.clip = track;

        UpdateTimeFrame();

        source.Play();
    }

    private void StopSequence()
    {
        shooter.DeActivate();

        source.Stop();
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
        if (!source.isPlaying)
        {
            return;
        }

        if (sequenceIndex >= sequence.shooterTimeFrames.Length)
        {
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
        shooter.shotDelay = GetShotDelay(sequence.shooterTimeFrames[sequenceIndex].subdivision);
        shooter.projectileSpeed = sequence.shooterTimeFrames[sequenceIndex].projectileSpeed;

        if (shooter is SwoopingShooter sweeper)
        {
            sweeper.frequency = sequence.shooterTimeFrames[sequenceIndex].waveFrequency;
        }

        shooter.SetColor(sequence.shooterTimeFrames[sequenceIndex].color);
    }
}
