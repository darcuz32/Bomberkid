using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Explosion : MonoBehaviour
{
    int modificadorDamage;
    [SerializeField]
    AnimationClip explosion;

    public void Init(int modificadorDamage)
    {
        this.modificadorDamage = modificadorDamage;
        StartCoroutine(EjecutarContador(explosion));
    }

    IEnumerator EjecutarContador(AnimationClip animation)
    {
        yield return new WaitForSeconds(explosion.length);
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
        else if (collision.tag == "Pared destruible")
        {
            Tilemap tilemap = collision.GetComponent<Tilemap>();
            Vector3Int pos = Vector3Int.FloorToInt(transform.position);
            tilemap.SetTile(pos, null);
            //Destroy(collision.gameObject);
        }
        else if (collision.tag == "Bomba")
        {
            collision.SendMessage("Activar");
        }
        else if (collision.tag != "Explosion")
        {
            // Destruir();
        }



    }
}
