using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreTracker : MonoBehaviour
{
    public float pointsPerHit = 100f;
    public TextMeshProUGUI scoreText;
    public float resetTickTime = 1f;
    public int faceTransitionStep = 20;
    public Material faceMaterial;

    public Texture2D[] faces;

    [HideInInspector]
    public float score = 0;
    private int timesHit;

    private float nextResetTime = 0;
    private int damageFaceValue;

    private int currentFaceIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameRunning)
        {
            damageFaceValue = 0;
        }

        if (Time.time >= nextResetTime)
        {
            damageFaceValue = Mathf.Max(0, damageFaceValue - 1);
            nextResetTime = Time.time + resetTickTime;
        }

        UpdateFaceTexture();
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile p = other.gameObject.GetComponent<Projectile>();

        if (p != null)
        {
            score += pointsPerHit;
            timesHit++;
            UpdateScoreText();
            OnProjectileHit(p);
        }
    }

    private void OnProjectileHit(Projectile p)
    {
        p.Die();

        GameManager.instance.UpdateShotsHit();

        damageFaceValue++;
    }

    private void UpdateFaceTexture()
    {
        int highThreshold = currentFaceIndex * faceTransitionStep;
        int lowThreshold = currentFaceIndex * faceTransitionStep - 1;
        if (currentFaceIndex == 0)
        {
            lowThreshold = 0;
        }

        if (damageFaceValue > highThreshold)
        {
            currentFaceIndex = Mathf.Clamp(currentFaceIndex + 1, 0, faces.Length - 1);
        }
        else if (damageFaceValue < lowThreshold)
        {
            currentFaceIndex = Mathf.Clamp(currentFaceIndex - 1, 0, faces.Length - 1);
        }

        Debug.Log($"Face Index: {currentFaceIndex} high {highThreshold} val: {damageFaceValue}");

        Texture t = faces[currentFaceIndex];

        faceMaterial.mainTexture = t;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Damage: " + score.ToString();
    }
}
