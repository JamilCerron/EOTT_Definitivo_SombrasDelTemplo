using UnityEngine;

public class Escopeta : Arma
{
    protected override void AliniearArma()
    {
        // Alinear la escopeta con la c�mara
        transform.position = camara.position + camara.TransformDirection(offsetEScopeta);

        // Rotar la escopeta con la c�mara, pero suavemente
        transform.rotation = Quaternion.Lerp(transform.rotation, camara.rotation, Time.deltaTime * 10f);
    }

}
