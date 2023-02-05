using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;

    public float force;
    public float lifeTime;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        StartCoroutine(lifeTimeTimer());
    }

    IEnumerator lifeTimeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
