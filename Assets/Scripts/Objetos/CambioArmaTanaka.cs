using UnityEngine;

public class CambioArmaTanaka : MonoBehaviour
{

    [SerializeField] private GameObject escopeta;
    [SerializeField] private GameObject crucifijo;

    private int currentWeaponIndex;

    private bool escopetaPermitido;
    private bool crucifijoPermitido;

    [SerializeField] private static float tiempoUsoCrucifijo = 10;  // Duración del uso del crucifijo
    [SerializeField] private static float temporizadorCrucifijo = 0f;  // Control del tiempo restante de uso del crucifijo

    public void Awake()
    {
        currentWeaponIndex = 0;
    }

    public void Update()
    {
        SwitchWeapon();
        CrucifijoUso();
    }

    // Método para reactivar el crucifijo al obtener uno nuevo
    public void ReactivarCrucifijo()
    {
        temporizadorCrucifijo = tiempoUsoCrucifijo;  // Reiniciar el temporizador
    }

    public void CrucifijoUso()
    {
        if (currentWeaponIndex == 1 && temporizadorCrucifijo > 0)
        {
            temporizadorCrucifijo -= Time.deltaTime;
        }

        if (temporizadorCrucifijo <= 0f)
        {
            currentWeaponIndex = 0;
        }
    }



    public float TiempoCrucifijo()
    {
        return temporizadorCrucifijo;
    }

    public bool EsEscopetaActiva()
    {
        return escopetaPermitido;
    }

    private void SwitchWeapon()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeaponIndex = 2;
        }
        */
        if (Input.mouseScrollDelta.y > 0)
        {
            currentWeaponIndex++;
            if (currentWeaponIndex > 2)
            {
                currentWeaponIndex = 0;
            }
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = 2;
            }
        }

        switch (currentWeaponIndex)
        {
            case 0:
                escopeta.SetActive(false);
                crucifijo.SetActive(false);
                break;
            case 1:
                if (escopetaPermitido && !escopeta.activeSelf)
                {
                    escopeta.SetActive(true);
                    crucifijo.SetActive(false);
                }
                break;
            case 2:
                if (crucifijoPermitido && !crucifijo.activeSelf)
                {
                    crucifijo.SetActive(true);
                    escopeta.SetActive(false);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("RangoEscopeta"))
        {
            Destroy(collision.gameObject);
            escopetaPermitido = true;
        }

        if (collision.gameObject.CompareTag("RangoCrucifijo"))
        {
            Destroy(collision.gameObject);
            crucifijoPermitido = true;
            ReactivarCrucifijo();
        }
    }
}
