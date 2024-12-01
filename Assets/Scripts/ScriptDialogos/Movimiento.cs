using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidad = 15f;

    void Update()
    {
        //Captura el movimiento del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Calcular el movimiento
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        //Aplicar movimiento
        transform.Translate(movement * velocidad * Time.deltaTime, Space.World);


    }
}
