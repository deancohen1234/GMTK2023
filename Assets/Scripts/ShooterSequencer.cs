using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSequencer : MonoBehaviour
{
    public Shooter shooter;
    public AudioClip track;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!shooter.isActive)
            {
                shooter.Activate();
            }
            else
            {
                shooter.DeActivate();
            }
        }
    }
}
