using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFinalApagar : MonoBehaviour
{
    [SerializeField] private GameObject panelFinal;

    void Start()
    {
        panelFinal.SetActive(false);
    }
}
