using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] protected Rigidbody ammoRB;
    [SerializeField] protected float ammoForce;
    [SerializeField] protected bool directionRight;
    [SerializeField] protected GameObject hitVFX;
    [SerializeField] public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAmmo", 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ShipScript>().ChangeHealth(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().ChangeHealth(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        else if (other.gameObject.CompareTag("Ammo"))
        {
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
       
    }

    private void DestroyAmmo()
    {
        Destroy(gameObject);
    }

}
