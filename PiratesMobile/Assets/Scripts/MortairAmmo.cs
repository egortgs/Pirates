using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class MortairAmmo : MonoBehaviour
{
    [SerializeField] private float ammoMoveTime;
    MortairCamera mortairCamera;
    Vector3 pointTarget;


   

    // Start is called before the first frame update
    void Start()
    {
       mortairCamera = FindObjectOfType<MortairCamera>();
        pointTarget = mortairCamera.hitPos.position;
        Invoke("DestroyAmmo", 10f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.Slerp(transform.position, pointTarget, ammoMoveTime);
        transform.position = Vector3.MoveTowards(transform.position, pointTarget, ammoMoveTime);
        if (transform.position.z == pointTarget.z)
        {
            transform.position = transform.forward * ammoMoveTime *Time.deltaTime;
        }
    }

    private void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
