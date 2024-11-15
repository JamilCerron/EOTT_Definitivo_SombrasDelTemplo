using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianCabezazo : MonoBehaviour
{
    public float attackForce = 10f;
    private bool canAttack = false;
    private Vector3 initialPosition; 
    private Guardian guardian;

    void Start()
    {
        initialPosition = transform.position; 
        guardian = GetComponent<Guardian>(); 
    }


    void Update()
    {
        if (canAttack)
        {
            // Realiza la embestida
            Vector3 direction = (GameObject.FindWithTag("PLAYER").transform.position - transform.position).normalized;
            GetComponent<Rigidbody>().AddForce(direction * attackForce, ForceMode.Impulse);
            canAttack = false;
        }

        else if (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            Vector3 direction = (initialPosition - transform.position).normalized;
            transform.position += direction * guardian.speed * Time.deltaTime;
        }
    }

    public void AttemptAttack()
    {
        canAttack = true; // Método para iniciar el ataque desde el script del guardián
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PLAYER"))
        {
            // Empuja al jugador
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushDirection = collision.transform.position - transform.position;
            playerRb.AddForce(pushDirection.normalized * attackForce, ForceMode.Impulse);
        }
    }
}
