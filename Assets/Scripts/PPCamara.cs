using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPCamara : MonoBehaviour
{
    private  Transform camara;
    public Vector2 sensibilidad;

    // Start is called before the first frame update
    void Start()
    {
        camara = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotación del jugador en el eje Y (horizontal)
        float hor = Input.GetAxis("Mouse X");
        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensibilidad.x);
        }

        // Rotación de la cámara en el eje X (vertical)
        float ver = Input.GetAxis("Mouse Y");
        if (ver != 0)
        {
            float angle = (camara.localEulerAngles.x - ver * sensibilidad.y + 360) % 360;
            if (angle > 180)
            {
                angle -= 360;
            }
            angle = Mathf.Clamp(angle, -80, 80); // Limitar el ángulo de rotación vertical
            camara.localEulerAngles = Vector3.right * angle;
        }
    }


    //float hor = Input.GetAxis("Mouse X");
    //float ver = Input.GetAxis("Mouse Y");

    //if(hor !=0)
    //{
    //    transform.Rotate(Vector3.up * hor * sensibilidad.x);
    //}

    //if (ver != 0)
    //{
    //   // camara.Rotate(Vector3.left * ver * sensibilidad.y);
    //    float angle = (camara.localEulerAngles.x - ver * sensibilidad.y + 360) % 360;
    //    if(angle > 180)
    //    {
    //        angle -= 360;
    //    }
    //    angle = Mathf.Clamp(angle, -80, 80);

    //    camara.localEulerAngles = Vector3.right * angle;

    //}




}

