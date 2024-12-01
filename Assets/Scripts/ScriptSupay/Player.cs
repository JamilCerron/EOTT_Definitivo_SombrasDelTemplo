using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Rigidbody rb;
    private Vector3 movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");


        movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical).normalized;
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movimiento * velocidadMovimiento * Time.fixedDeltaTime);
    }

}
