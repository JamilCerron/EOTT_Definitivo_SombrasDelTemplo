using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinicioCursor : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
