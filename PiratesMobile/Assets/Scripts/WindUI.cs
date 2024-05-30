using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindUI : MonoBehaviour
{
    [SerializeField] WindDirection windDirection;
    [SerializeField] Camera cam;
    Vector3 dir;

    void Update()
    {
        dir.z = windDirection.windDir.z;
        transform.localEulerAngles = dir;
    }
}
