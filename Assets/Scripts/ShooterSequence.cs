using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShooterTimeFrame
{
    public enum Subdivision { Whole, Half, Quater, Eighth, Sixteenth}

    public Subdivision subdivision = Subdivision.Quater;
    public float endTime;

    public float shotDelay;
    public float waveFrequency;
    public float projectileSpeed;
    public Color color;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShooterSequence", order = 1)]
public class ShooterSequence : ScriptableObject
{
    public AudioClip song;

    public float songBPM = 120;
    public ShooterTimeFrame[] shooterTimeFrames;
}
