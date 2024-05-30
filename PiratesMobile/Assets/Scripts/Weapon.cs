using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    InputSystem controls;
    [SerializeField] protected ParticleSystem shootVFX;
    [SerializeField] protected GameObject ammo;

    [SerializeField] protected float cooldown;
    protected float timer = 0;

    [SerializeField] protected bool rightSide;
    [SerializeField] protected AudioSource sound;
    [SerializeField] protected AudioClip shootSound;
    void Awake()
    {
        controls = new InputSystem();
        controls.Enable();

        controls.Ship.ShootRight.performed += ctx => ShootRight();
        controls.Ship.ShootLeft.performed += ctx => ShootLeft();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;     
        if (Input.GetKeyDown(KeyCode.E) && rightSide)
        {
            ShootRight();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !rightSide)
        {
            ShootLeft();
        }
    }

    public void ShootRight()
    {
        if (timer > cooldown && rightSide)
        {
            OnShoot();
            timer = 0;
        }
    }

    public void ShootLeft()
    {
        if (timer > cooldown && !rightSide)
        {
            OnShoot();
            timer = 0;
        }
    }

    protected virtual void OnShoot()
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
