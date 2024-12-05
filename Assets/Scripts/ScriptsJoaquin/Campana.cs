using UnityEngine;

public class Campana : MonoBehaviour
{
    private GestorCampana gestorJuego;
    private bool activada = false;

    //[SerializeField] private GameObject señal;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        //señal.SetActive(false);
    }

    public void SetGestorJuego(GestorCampana gestor)
    {
        gestorJuego = gestor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala") && !activada)
        {
            //señal.SetActive(true);
            Destroy(other.gameObject);

            fxSource.PlayOneShot(clickSound);
            activada = true;
            Debug.Log("Campana activada.");
            gestorJuego.ActivarCampana(this);

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        señal.SetActive(false);
    //    }
    //}

    public void Resetear()
    {
        activada = false;
        Debug.Log("Campana reseteada.");
    }
}
