using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirection : MonoBehaviour
{
    [SerializeField] public Vector3 windDir;
    [SerializeField] public float windSpeed;
    float windTimer = 0;
    [SerializeField] public float windCooldown;


    private void Start()
    {
        ChangeWind();
    }
    // Update is called once per frame
    void Update()
    {
        windTimer += Time.deltaTime;
        if (windTimer > windCooldown)
        {
            ChangeWind();
            windTimer = 0;
        }
    }



    void ChangeWind()
    {
        windDir = new Vector3(0, 0, Random.Range(1, 359));
        windSpeed = Random.Range(1f, 7f);
    }
}
