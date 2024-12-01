using UnityEngine;

public class MunicionEscopetaJ : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var disparoEscopeta = other.GetComponent<DisparoEscopetaJ>();
            if (disparoEscopeta != null)
            {
                disparoEscopeta.AumentarMunicion(8);
                Destroy(gameObject);
            }
        }
    }
}
