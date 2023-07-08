using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;

    public float shotDelay = 0.5f;

    private float nextShotTime;
    private int objectPoolerIndex;

    // Start is called before the first frame update
    void Start()
    {
        objectPoolerIndex = ObjectPooler.SharedInstance.AddObject(projectile, 5);
    }

    // Update is called once per frame
    void Update()
    {
        FireUpdate();
    }

    private void FireUpdate()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotDelay;
            FireProjectile();
        }
    }

    

    private void FireProjectile()
    {
        Projectile p = ObjectPooler.SharedInstance.GetPooledObject(objectPoolerIndex).GetComponent<Projectile>();

        //set direction and fire
        p.gameObject.SetActive(true);
        p.transform.position = transform.position;
        p.transform.forward = Vector3.forward;
        p.Fire();
    }
}
