using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance, currentDistance, detectionDistance;
    [SerializeField] protected float cooldown;
    protected float timer;

    [SerializeField] protected float speed, currentSpeed, neutralSpeed, underSpeed;
    protected bool isDead;
    protected Animator anim;
    protected Rigidbody rb;
    [SerializeField] protected Transform targetMove;
    float patrolTimer = 0;
    [SerializeField] float freeSailTimer;

    [SerializeField] protected GameObject player;
    [SerializeField] protected Transform spawn;
    [SerializeField] protected int rotSpeed;
    [SerializeField] protected List<GameObject> canons = new List<GameObject>();
    [SerializeField] protected GameObject ammo;
    [SerializeField] protected GameObject rightTarget;
    [SerializeField] protected GameObject leftTarget;

    [SerializeField] protected AudioSource sound;
    [SerializeField] protected AudioClip shootSound;

    [SerializeField] ParticleSystem wavesVFX1, wavesVFX2, damageVFX, destroy1VFX, destroy2VFX, hitVFX;
    [SerializeField] AudioSource shipSound;
    [SerializeField] AudioClip hitSFX, destroySFX;

    protected RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        currentDistance = Vector3.Distance(transform.position, player.transform.position);
        patrolTimer = freeSailTimer;
    }

    // Update is called once per frame
    void Update()
    {

        currentDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!isDead)
        {
            Attack();
        }
        if (currentSpeed < neutralSpeed)
        {
            currentSpeed = neutralSpeed;
        }
        if (currentSpeed > speed)
        {
            currentSpeed = speed;
        }

        if (patrolTimer > freeSailTimer)
        {
            ChangeTargetPosition();
            patrolTimer = 0;
        }
        else
        {
            patrolTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
            FreeSail();
        }
        else
        {
            rb.MovePosition(transform.position - transform.up * underSpeed * Time.deltaTime);
        }
        AnimationControllers();
    }

    protected virtual void Move()
    {

    }

    protected virtual void Attack()
    {
       
    }

    protected virtual void OnShootRight()
    {

    }

    protected virtual void OnShootLeft()
    {

    }

    protected virtual void FreeSail()
    {

    }

    public void ChangeTargetPosition()
    {
            targetMove.position = new Vector3(Random.Range(-300, 300), 0, Random.Range(-300, 300));
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
            shipSound.PlayOneShot(destroySFX);
            isDead = true;
            destroy2VFX.Play();
            anim.SetBool("dead", true);
            rb.constraints = RigidbodyConstraints.None;
            Invoke("DestroyShip", 5f);
            
        }
    }

    public void DestroyShip()
    {
            Destroy(gameObject);
    }



    private void AnimationControllers()
    {
        if (!isDead)
        {
            anim.SetFloat("acceleration", currentSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ChangeTargetPosition();
        }
    }
}
