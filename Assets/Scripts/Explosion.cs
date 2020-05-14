using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    int modificadorDamage;

    public void Init(int modificadorDamage)
    {
        this.modificadorDamage = modificadorDamage;
        StartCoroutine(EjecutarContador());
    }

    IEnumerator EjecutarContador()
    {
        yield return new WaitForSeconds(2);
        Destruir();
    }

    void Destruir()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            collision.SendMessage("Destruir");
        }
        else if (collision.tag == "ParedDestructible")
        {

            Destroy(collision.gameObject);
        }
        else if (collision.tag != "Explosion")
        {
            Destruir();
        }

        
    }
}
