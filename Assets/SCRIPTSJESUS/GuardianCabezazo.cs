using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianCabezazo : MonoBehaviour
{
    public float attackForce = 10f;
    private bool canAttack = false;

    void Update()
    {
        if (canAttack)
        {
            // Realiza la embestida
            Vector3 direction = (transform.position - GameObject.FindWithTag("PLAYER").transform.position).normalized;
            GetComponent<Rigidbody>().AddForce(direction * attackForce, ForceMode.Impulse);
            canAttack = false;
        }
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
