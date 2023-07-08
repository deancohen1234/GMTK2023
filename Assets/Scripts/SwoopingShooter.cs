using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwoopingShooter : Shooter
{
    public float frequency = 1;
    public float angleDamper = 0.5f;

    protected override void FireProjectile(Projectile p)
    {
        float sin = Mathf.Sin(Time.time * frequency);
        float cos = Mathf.Cos(Time.time * frequency);

        Vector3 direction = new Vector3(cos * angleDamper, 0f, 1f).normalized;
            //set direction and fire
        p.gameObject.SetActive(true);
        p.transform.position = transform.position;
        p.transform.forward = direction;
        p.Fire(projectileSpeed);

        p.GetComponent<MeshRenderer>().material.color = color;
    }
}
