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
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animCont = GetComponent<Animator>();
        offset = transform.GetChild(0);
    }

    public void Init(float tiempoDeDetonacion, int modificadorDamage, string elQueColoco, int alcance, GameObject explosion)
    {
        this.tiempoDeDetonacion = tiempoDeDetonacion;
        this.modificadorDamage = modificadorDamage;
        this.elQueColoco = elQueColoco; 
        this.alcance = alcance;
        this.explosion = explosion;
        animCont.SetBool("Inestable", false);

        StartCoroutine(ActivarBomba());
    }


    IEnumerator ActivarBomba()
    {
        yield return new WaitForSeconds(tiempoDeDetonacion - 3);
        animCont.SetBool("Inestable", true);
        yield return new WaitForSeconds(3);
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


}
