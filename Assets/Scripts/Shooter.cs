using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShooterType { SinSweep, Random}

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public ShooterType shooterType = ShooterType.SinSweep;
    public float shotDelay = 0.5f;
    public float projectileSpeed = 6f;
    public float frequency = 1;
    public float angleDamper = 0.5f;
    [HideInInspector]
    public Color color;

    private float nextShotTime;
    private int objectPoolerIndex;
    protected int shotsFired;

    public bool isActive { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        objectPoolerIndex = ObjectPooler.SharedInstance.AddObject(projectile, 30);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            FireUpdate();
        }
    }

    public void Activate()
    {
        isActive = true;

        //reset shots fired
        shotsFired = 0;
    }

    public void DeActivate()
    {
        isActive = false;
    }

    public void SetColor(Color _color)
    {
        color = _color;
    }

    public int GetShotsFired()
    {
        return shotsFired;
    }

    private void FireUpdate()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotDelay;
            Projectile p = ObjectPooler.SharedInstance.GetPooledObject(objectPoolerIndex).GetComponent<Projectile>();
            FireProjectile(p);
        }
    }

    protected void FireProjectile(Projectile p)
    {
        switch (shooterType)
        {
            case ShooterType.SinSweep:
                SinSweepFireProjectile(p);
                break;
            case ShooterType.Random:
                RandomFireProjectile(p);
                break;
        }

        //set direction and fire
        p.gameObject.SetActive(true);
        p.transform.position = transform.position;
        p.Fire(projectileSpeed);

        p.GetComponent<MeshRenderer>().material.color = color;
        shotsFired++;
    }

    private void SinSweepFireProjectile(Projectile p)
    {
        float sin = Mathf.Sin(Time.time * frequency);
        float cos = Mathf.Cos(Time.time * frequency);

        Vector3 direction = new Vector3(cos * angleDamper, 0f, 1f).normalized;

        //set direction
        p.transform.forward = direction;
    }

    private void RandomFireProjectile(Projectile p)
    {
        float randomX = Random.Range(-angleDamper, angleDamper);
        float randomY = Random.Range(-angleDamper, angleDamper);
        float sin = Mathf.Sin(randomX);
        float cos = Mathf.Cos(randomY);

        Vector3 direction = new Vector3(sin, 0, cos).normalized;

        //set direction
        p.transform.forward = direction;
    }
}
