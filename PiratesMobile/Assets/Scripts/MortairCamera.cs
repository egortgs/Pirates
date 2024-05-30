using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GridBrushBase;

public class MortairCamera : MonoBehaviour
{
    InputSystem controls;
    [SerializeField] private float rotateSpeed = 1f;

    [SerializeField] GameObject targetObject;
    private Vector3 rotationDirection;
    private float rotX, rotY;
    private float currentRotationX, currentRotationY;

    RaycastHit hit;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private GameObject exp;
    [SerializeField] private GameObject ammoMortair;
    [SerializeField] private GameObject springRay;
    [SerializeField] private Transform mortair;
    [SerializeField] private Transform endPos;
    [SerializeField] private float ammoMoveTime;

    [SerializeField] Canvas mainCanvas, mortairCanvas;

    [SerializeField] CinemachineFreeLook mainCam;
    [SerializeField] CinemachineVirtualCamera mortairCamera;

    Vector3 mortairPos;
    Vector3 centerPoint;

    [SerializeField] private float minValueX = 0f;
    [SerializeField] private float maxValueX = 5f;
    [SerializeField] private float minValueY = 0f;
    [SerializeField] private float maxValueY = 5f;

    [SerializeField] public Transform hitPos;
    [SerializeField] private GameObject mortairCanon;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float cooldown;
    private float timer;

    [SerializeField] protected AudioSource sound;
    [SerializeField] protected AudioClip shootSound;
    void Awake()
    {
        controls = new InputSystem();
        controls.Enable();

        controls.Ship.StopMortair.performed += ctx => MortairUI();
        controls.Ship.ShootMortair.performed += ctx => MortairShoot();
        controls.Ship.MortairRotateX.performed += ctx =>
        {
            rotX = ctx.ReadValue<float>();
        };
        controls.Ship.MortairRotateY.performed += ctx =>
        {
            rotY = ctx.ReadValue<float>();
        };

    }
    // Start is called before the first frame update
    void Start()
    {
        targetObject.SetActive(false);
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        rotationDirection = new Vector3(rotX, rotY, 0);
                     
        targetObject.transform.position = hit.point;
                      
        currentRotationY += rotateSpeed * rotationDirection.y * Time.deltaTime;
        currentRotationX += rotateSpeed * rotationDirection.x * Time.deltaTime;

        currentRotationX = Mathf.Clamp(currentRotationX, minValueX, maxValueX);
        currentRotationY = Mathf.Clamp(currentRotationY, minValueY, maxValueY);

        RotateMortair();
        ReadyShoot();
        hitPos.position = hit.point;
    }
    void ReadyShoot()
    {
        Ray ray = new Ray(mortairCanon.transform.position, springRay.transform.forward);
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {                   
            if (Input.GetKeyDown(KeyCode.J))
            {             
                MortairShoot();
            }
        }
        Debug.DrawRay(ray.origin, ray.direction);

    }

    private void MortairShoot()
    {
        if (cooldown < timer)
        {
            GameObject ball = Instantiate(ammoMortair, mortair.position, mortair.rotation);
            timer = 0;
            sound.PlayOneShot(shootSound);
        }         
    }

    public void RotateMortair()
    {
        springRay.transform.localRotation = Quaternion.Euler(currentRotationX, currentRotationY, 0.0f);
    }

    private void MortairUI()
    {
        targetObject.SetActive(false);
        mortairCamera.enabled = false;
        mainCanvas.enabled = true;
        mortairCanvas.enabled = false;
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
