using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    //past this z, kill projectile
    public float zDeath = 26f;

    private bool isFired = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFired)
        {
            //move forward at a set speed
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        DeathCheck();

    }


    public void Fire()
    {
        isFired = true;
    }

    public void Die(bool playEffects = true)
    {
        isFired = false;
        gameObject.SetActive(false);
    }

    private void DeathCheck()
    {
        if (transform.position.z >= zDeath)
        {
            Die(false);
        }
    }
}
