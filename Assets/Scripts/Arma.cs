using UnityEngine;

public class Arma : MonoBehaviour
{
    public Transform camara;    // La c�mara en primera persona
    public Vector3 offsetEScopeta = new Vector3(0.5f, -0.5f, 1f);    // Ajuste de posici�n para que la escopeta est� en el lugar correcto
    public Vector3 offsetCrucifijo = new Vector3(0.5f, -0.5f, 0.65f);

    void Update()
    {
        AliniearArma();
    }

    protected virtual void AliniearArma()
    {

    }
}
