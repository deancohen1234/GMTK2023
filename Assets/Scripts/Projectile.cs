using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //past this z, kill projectile
    public float zDeath = 26f;

    private float speed;
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


    public void Fire(float _speed)
    {
        speed = _speed;
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
