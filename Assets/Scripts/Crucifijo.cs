using UnityEngine;

public class Crucifijo : Arma
{
    public bool clickRealizado = false; // Variable para controlar el cambio del offset
    public GameObject efecto; // Prefab del efecto de disparo del crucifijo

    void OnEnable()
    {
        // Establecer el offset inicial del crucifijo al activarse
        offsetCrucifijo = new Vector3(-0.5f, -0.5f, 0.65f);
    }

    void Update()
    {
        // Detectar clic izquierdo para cambiar el offset y disparar
        if (Input.GetMouseButtonDown(0) && !clickRealizado)
        {
            offsetCrucifijo = new Vector3(-0.5f, -0.5f, 1f); // Cambiar el offset al hacer clic
            Disparar(); // Llamar al método de disparo
            clickRealizado = true;
        }

        // Restaurar el offset al soltar el clic izquierdo
        if (Input.GetMouseButtonUp(0) && clickRealizado)
        {
            offsetCrucifijo = new Vector3(-0.5f, -0.5f, 0.65f); // Volver al offset inicial al soltar
            clickRealizado = false;
        }

        // Llamar a la lógica de alineación del arma
        AliniearArma();
    }

    protected override void AliniearArma()
    {
        // Alinear el crucifijo con la cámara
        transform.position = camara.position + camara.TransformDirection(offsetCrucifijo);
        transform.rotation = Quaternion.Lerp(transform.rotation, camara.rotation, Time.deltaTime * 10f);
    }

    protected virtual void Disparar()
    {
        // Aquí puedes implementar la lógica de disparo del crucifijo, como instanciar un efecto
        if (efecto != null)
        {
            Instantiate(efecto, transform.position, transform.rotation);
        }
    }

}
