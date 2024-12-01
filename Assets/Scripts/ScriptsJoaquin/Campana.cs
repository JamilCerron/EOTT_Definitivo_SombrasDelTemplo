using UnityEngine;

public class Campana : MonoBehaviour
{
    private GestorCampana gestorJuego;
    private bool activada = false;

    [SerializeField] private GameObject se�al;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        se�al.SetActive(false);
    }

    public void SetGestorJuego(GestorCampana gestor)
    {
        gestorJuego = gestor;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !activada)
        {
            se�al.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                fxSource.PlayOneShot(clickSound);
                activada = true;
                Debug.Log("Campana activada.");
                gestorJuego.ActivarCampana(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            se�al.SetActive(false);
        }
    }

    public void Resetear()
    {
        activada = false;
        Debug.Log("Campana reseteada.");
    }
}
