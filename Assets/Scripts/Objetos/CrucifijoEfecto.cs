using UnityEngine;

public class CrucifijoEfecto : MonoBehaviour
{
    [SerializeField] private GameObject rangoEfecto;
    private bool estaEnUso = false;

    private CambioArmaTanaka cambioArma;

    private void Awake()
    {
        gameObject.SetActive(false);
        cambioArma = GameObject.FindGameObjectWithTag("Player").GetComponent<CambioArmaTanaka>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (cambioArma.TiempoCrucifijo() > 0)
            {
                gameObject.SetActive(true);
                estaEnUso = true;
            }
        }
    }

    public bool enUso()
    {
        return estaEnUso;
    }
}
