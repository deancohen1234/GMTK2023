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

    private const int TOTALSHOTS = 729;

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
        float percentOfMaxShots = (float)damageFaceValue / TOTALSHOTS;

        if (percentOfMaxShots < 0.2f)
        {
            currentFaceIndex = 0;
        }
        else if (percentOfMaxShots >= 0.2f && percentOfMaxShots < 0.4f)
        {
            currentFaceIndex = 1;
        }
        else if (percentOfMaxShots >= 0.4f && percentOfMaxShots < 0.6f)
        {
            currentFaceIndex = 2;
        }
        else if (percentOfMaxShots >= 0.6f && percentOfMaxShots < 0.8f)
        {
            currentFaceIndex = 3;
        }
        else
        {
            currentFaceIndex = 4;
        }

        Texture t = faces[currentFaceIndex];

        faceMaterial.mainTexture = t;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Damage: " + score.ToString();
    }
}
