using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShooterTimeFrame
{
    public enum Subdivision { Whole, Half, Quater, Eighth, Sixteenth}

    public Subdivision subdivision = Subdivision.Quater;
    public ShooterType shooterType = ShooterType.SinSweep;
    public float endTime;

    public float waveFrequency;
    public float projectileSpeed;
    public Color color;
    public float angleDampen;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShooterSequence", order = 1)]
public class ShooterSequence : ScriptableObject
{
    public AudioClip song;

    public float songBPM = 120;
    public ShooterTimeFrame[] shooterTimeFrames;
}
