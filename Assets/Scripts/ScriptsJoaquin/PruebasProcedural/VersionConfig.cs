using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaVersionConfig", menuName = "Configuraciones/VersionConfig")]
public class VersionConfig : ScriptableObject
{
    public GameObject[] tiposDeObjetos;
    public Vector3[] posiciones;
}
