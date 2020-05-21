using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    float tiempoDeDetonacion;
    int modificadorDamage;
    string elQueColoco;
    Rigidbody2D rb2d;
    Animator animCont;
    int alcance;
    Transform offset;
    GameObject explosion;
    Coroutine detonar;
    [SerializeField]
    bool activada = true;
    float tiempoFaltante;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        offset = transform.GetChild(0);
        animCont = GetComponent<Animator>();
    }

    public void Init(float tiempoDeDetonacion, int modificadorDamage, string elQueColoco, int alcance, GameObject explosion)
    {
        this.tiempoDeDetonacion = tiempoDeDetonacion;
        this.modificadorDamage = modificadorDamage;
        this.elQueColoco = elQueColoco; 
        this.alcance = alcance;
        this.explosion = explosion;
        animCont.SetBool("Critica", false);
        activada = true;
        detonar = StartCoroutine(ActivarBomba(tiempoDeDetonacion));
    }


    IEnumerator ActivarBomba(float tiempoDeDetonacion)
    {
        for (float tiempoFaltante = tiempoDeDetonacion; tiempoFaltante > 3; tiempoFaltante -= Time.deltaTime)
        {
            this.tiempoDeDetonacion = tiempoFaltante;
            yield return null;
        }
        if (!animCont.GetBool("Critica"))
        {
            animCont.SetBool("Critica", true);
            yield return new WaitForSeconds(3);
        }
        Detonar();
    }

    void Detonar()
    {

        Destroy(gameObject);
        Vector3 pos = offset.position;

        
        CrearExplosion(pos);

        for (int i = 1; i <= alcance; i++)
         {
             pos = offset.position;
             pos.x += i;
             CrearExplosion(pos);

             pos = offset.position;
             pos.x -= i;
             CrearExplosion(pos);

             pos = offset.position;
             pos.y += i;
             CrearExplosion(pos);

             pos = offset.position;
             pos.y -= i;
             CrearExplosion(pos);


         }
        
    }

    void CrearExplosion(Vector3 pos)
    {
        
        GameObject explosionParaMostrar = Instantiate(explosion, pos, new Quaternion());
        explosionParaMostrar.GetComponent<Explosion>().Init(modificadorDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.SendMessage("InteraccionBomba", gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.SendMessage("InteraccionBomba", gameObject);
        }

    }

    void Interaccion(GameObject gameObject)
    {
        activada = !activada;
        animCont.SetTrigger("Cambio");
        if (!activada)
        {
            StopCoroutine(detonar);
            gameObject.SendMessage("CambioDeVelocidad", -2);
        }
        else
        {
            detonar = StartCoroutine(ActivarBomba(tiempoDeDetonacion));
            gameObject.SendMessage("CambioDeVelocidad", 2);
        }

        
    }

    void Activar()
    {
        
        if (!activada) {
            activada = !activada;
            animCont.SetTrigger("Cambio");
            detonar = StartCoroutine(ActivarBomba(tiempoDeDetonacion));
        }


    }


}
