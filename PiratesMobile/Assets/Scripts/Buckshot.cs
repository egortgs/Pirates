using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buckshot : MonoBehaviour
{
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private int damage;
    private void Start()
    {
        damage = 5;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().ChangeHealth(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ShipScript>().ChangeHealth(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(gameObject);
        }

    }

   
}
