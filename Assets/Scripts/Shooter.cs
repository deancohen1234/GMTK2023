using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public float shotDelay = 0.5f;
    public float projectileSpeed = 6f;
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

    

    protected virtual void FireProjectile(Projectile p)
    {
        //set direction and fire
        p.gameObject.SetActive(true);
        p.transform.position = transform.position;
        p.transform.forward = Vector3.forward;
        p.Fire(projectileSpeed);

        shotsFired++;

    }
}
