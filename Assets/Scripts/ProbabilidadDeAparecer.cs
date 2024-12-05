using UnityEngine;

public class ProbabilidadDeAparecer : MonoBehaviour
{
    [SerializeField] private float probabilidad = 0.7f;
    void Start()
    {

        float valorAleatorio = Random.value;


        if (valorAleatorio >= probabilidad)
        {
            Destroy(gameObject);
        }
    }
}
