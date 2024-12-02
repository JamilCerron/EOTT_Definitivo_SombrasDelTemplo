using UnityEngine;

public class BalaCabezaSupay : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().RecibirDaño(3);

        }

       Destroy(gameObject);

        
    }






}
