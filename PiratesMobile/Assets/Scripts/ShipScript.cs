using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShipScript : MonoBehaviour
{
    InputSystem controls;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] int health = 100;
    [SerializeField] Transform spawn;

    Vector3 rotationDirection;
    float rot;
    bool isDead = false;
    bool isStart, isBreak;
    bool isBreaking, isSlowdown, isSpeed1, isSpeed2;

    bool isSpeedLow, isSpeedHigh;
    int speed = 0;

    [SerializeField] CinemachineFreeLook mainCam;

    [SerializeField] GameObject targetObject;
    [SerializeField] GameObject waves1, waves2;
    [SerializeField] float currentSpeed, lowSpeed, highSpeed, neutralSpeed;
    [SerializeField] ParticleSystem wavesVFX1, wavesVFX2, damageVFX, destroy1VFX, destroy2VFX, hitVFX;
    [SerializeField] AudioSource shipSound, hitSound;
    [SerializeField] AudioClip sail1, sail2, hitSFX, destroySFX;

    

   //[SerializeField] WindDirection windDirection;
    void Awake()
    {
       
            controls = new InputSystem();
            controls.Enable();

            controls.Ship.StartMortair.performed += ctx => MortairUIStart();
            controls.Ship.MoveForward.performed += ctx => MoveSpeed();
            controls.Ship.Break.performed += ctx => BreakSpeed();
            
            controls.Ship.Rotate.performed += ctx =>
            {
                rot = ctx.ReadValue<float>();
            };
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            neutralSpeed = 0;
            currentSpeed = 0;
            speed = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
       
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rotationDirection = new Vector3(0, 1, 0);
                transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rotationDirection = new Vector3(0, -1, 0);
                transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.W) && speed == 0)
            {
                isSpeed1 = true;
                speed = 1;
                isSlowdown = false;
                isBreaking = false;
                isSpeed2 = false;
            }
            else if (Input.GetKeyDown(KeyCode.W) && speed == 1)
            {
                isSpeed2 = true;
                speed = 2;
                isSlowdown = false;
                isBreaking = false;
                isSpeed1 = false;
            }

            if (Input.GetKeyDown(KeyCode.S) && speed == 2)
            {
                isSlowdown = true;
                speed = 1;
                isBreaking = false;
                isSpeed1 = false;
                isSpeed2 = false;
            }
            else if (Input.GetKeyDown(KeyCode.S) && speed == 1)
            {
                isBreaking = true;
                speed = 0;
                isSlowdown = false;
                isSpeed1 = false;
                isSpeed2 = false;
            }

            if (isBreaking)
            {
                currentSpeed -= Time.deltaTime;
                if (currentSpeed < neutralSpeed)
                {
                    currentSpeed = neutralSpeed;
                    isBreaking = false;
                }
            }
            else if (isSlowdown)
            {
                currentSpeed -= Time.deltaTime;
                if (currentSpeed < lowSpeed)
                {
                    currentSpeed = lowSpeed;
                    isSlowdown = false;
                }
            }
            else if (isSpeed1)
            {
                currentSpeed += Time.deltaTime;

                if (currentSpeed > lowSpeed)
                {
                    currentSpeed = lowSpeed;
                    isSpeed1 = false;
                }

            }
            else if (isSpeed2)
            {
                currentSpeed += Time.deltaTime;
                if (currentSpeed > highSpeed)
                {
                    currentSpeed = highSpeed;
                    isSpeed2 = false;
                }
            }

            rotationDirection = new Vector3(0, rot, 0);

            RotateShip();
            AnimationControllers();

            if (currentSpeed > 0)
            {
                waves1.SetActive(true);
                waves2.SetActive(true);
            }
            else
            {
                waves1.SetActive(false);
                waves2.SetActive(false);
            }

            
          
        }
    }

    public void ChangeHealth(int count)
    {
        health -= count;

        if (health <= 50)
        {
            damageVFX.Play();
        }
        if (health <= 0)
        {
            destroy1VFX.Play();
            hitSound.PlayOneShot(destroySFX);
            isDead = true;
            Invoke("DestroyShip", 1f);
            
        }
    }

    void DestroyShip()
    {
        destroy2VFX.Play();
        anim.SetBool("dead", true);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            hitSound.PlayOneShot(hitSFX);
            
        }
    }

    private void AnimationControllers()
    {
        if (!isDead)
        {
            anim.SetFloat("acceleration", currentSpeed);
            anim.SetFloat("breaking", highSpeed - currentSpeed);
            anim.SetBool("isSailMove", isStart);
            anim.SetBool("isSailBreak", isBreak);
        }
    }

    void MoveSpeed()
    {
        if (!isDead)
        {
            if (speed == 0)
            {
                isSpeed1 = true;
                speed = 1;
                isSlowdown = false;
                isBreaking = false;
                isSpeed2 = false;
                isStart = true;
                isBreak = false;
                shipSound.PlayOneShot(sail1);
            }
            else if (speed == 1)
            {
                isSpeed2 = true;
                speed = 2;
                isSlowdown = false;
                isBreaking = false;
                isSpeed1 = false;
                isStart = true;
                isBreak = false;
                shipSound.PlayOneShot(sail2);
            }
        }
    }
  

    void BreakSpeed()
    {
        if (!isDead)
        {
            if (speed == 2)
            {
                isSlowdown = true;
                speed = 1;
                isBreaking = false;
                isSpeed1 = false;
                isSpeed2 = false;
                isStart = false;
                isBreak = true;
                shipSound.PlayOneShot(sail1);
            }
            else if (speed == 1)
            {
                isBreaking = true;
                speed = 0;
                isSlowdown = false;
                isSpeed1 = false;
                isSpeed2 = false;
                isStart = false;
                isBreak = true;
                shipSound.PlayOneShot(sail2);
            }
        }
    }


    private void RotateShip()
    {
        if (!isDead)
        {
            transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
        }
    }
    
    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.deltaTime);
            //rb.AddForce(windDirection.windDir * windDirection.windSpeed * Time.deltaTime);
        }
    }

    

    protected virtual void MortairUIStart()
    {
        
    }


    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }



}