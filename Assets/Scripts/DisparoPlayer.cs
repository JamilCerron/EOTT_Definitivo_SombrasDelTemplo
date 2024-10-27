using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlayer : Disparo
{
    [SerializeField] private GameObject PrefabBala;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private int BalasPlayer = 10;
    public float AlmacenBala = 0f;
    public float MaxAlamcenBalas = 50f;
    public float fuerzaDisparo = 20f;

    

    protected override void Disparar()
    {
        if(BalasPlayer > 0)
        {
           // Instanciar la bala en el punto de disparo
           GameObject balaObj = Instantiate(PrefabBala, ShootPoint.position, Quaternion.identity);

           // Obtener el componente Bala (o BalaPlayer) y establecer la direcci�n
           Bala bala = balaObj.GetComponent<Bala>();
           bala.SetDirection(ShootPoint.forward);  // Establecer la direcci�n hacia adelante desde el ShootPoint

           // Destruir la bala despu�s de 3 segundos
           Destroy(balaObj, 3f);

            //Restar Bala
            BalasPlayer--;
        }
        
        else if(BalasPlayer == 0 )
        {
            Console.WriteLine("Sin Balas");
        }
    }

    // M�todo para recargar
     protected override void Recarga()
    {
        if (AlmacenBala > 0 && BalasPlayer < 10)
        {
            // Calcula cu�ntas balas se necesitan para recargar al m�ximo de 10 balas
            int balasNecesarias = 10 - BalasPlayer;

            // Calcula cu�ntas balas se pueden tomar del almac�n
            int balasARecargar = Mathf.Min(balasNecesarias, (int)AlmacenBala);

            // Incrementa las balas en la escopeta
            BalasPlayer += balasARecargar;

            // Resta las balas usadas del almac�n
            AlmacenBala -= balasARecargar;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            // Aumentar el n�mero de balas al recoger munici�n
            AlmacenBala += 5;

            // Destruir el objeto de munici�n (opcional)
            Destroy(other.gameObject);
        }
    }

}
