using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public float pointsPerHit = 100f;
    public TextMeshProUGUI scoreText;


    [HideInInspector]
    public float score = 0;
    private int timesHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        GameManager.instance.UpdateShotsHit(timesHit);
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Damage: " + score.ToString();
    }
}
