using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public float pointsPerHit = 100f;
    public TextMeshProUGUI scoreText;

    private float score = 0;

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
            scoreText.text = "Damage: " + score.ToString();
            OnProjectileHit(p);
        }
    }

    private void OnProjectileHit(Projectile p)
    {
        p.Die();
    }
}
