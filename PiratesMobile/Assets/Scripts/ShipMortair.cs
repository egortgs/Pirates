using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMortair : ShipScript
{
    [SerializeField] CinemachineVirtualCamera mortairCamera;
    [SerializeField] GameObject targetMortair;
    [SerializeField] Canvas mainCanvas, mortairCanvas;

    private void Start()
    {
        mortairCanvas.enabled = false;
        mortairCamera.enabled = false;
    }

    protected override void MortairUIStart()
    {
        targetMortair.SetActive(true);
        mortairCamera.enabled = true;
        mortairCanvas.enabled = true;
        mainCanvas.enabled = false;
    }
}
