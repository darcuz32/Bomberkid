using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direccion { Arriba, Derecha, Abajo, Izquierda }

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField]
    [Range(200f, 1000f)]
    float velocidadJugador;

    Rigidbody2D rb2d;
    Animator animCont;

    [SerializeField]
    Direccion paDondeVeo;

    string jugadorAsignado;

    [SerializeField]
    AnimationClip chamuscado;

    bool muerto = false;
    Vector2 inputJugador;
    Coroutine cambiarVelocidad;

    public bool EstaMuerto
    {
        get { return muerto; }
    }


    public Direccion DireccionDeMovimiento
    {
        get { return paDondeVeo; }
    }

    public Vector2 DireccionDeMovimientoComoVector2
    {
        get
        {
            switch (paDondeVeo)
            {
                case Direccion.Arriba:
                    return Vector2.up;
                case Direccion.Abajo:
                    return Vector2.down;
                case Direccion.Derecha:
                    return Vector2.right;
                case Direccion.Izquierda:
                    return Vector2.left;
                default:
                    return Vector2.zero;
            }
        }
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animCont = GetComponent<Animator>();
        jugadorAsignado = name;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw(jugadorAsignado + "Vertical") != 0 && !muerto)
        {
            inputJugador = new Vector2(0, Input.GetAxisRaw(jugadorAsignado + "Vertical"));
        }
        else if (Input.GetAxisRaw(jugadorAsignado + "Horizontal") != 0 && !muerto)
        {
            inputJugador = new Vector2(Input.GetAxisRaw(jugadorAsignado + "Horizontal"), 0);

        }
        else
        {
            inputJugador = Vector2.zero;
        }

        rb2d.velocity = (inputJugador * velocidadJugador) * Time.deltaTime;

        if (inputJugador != Vector2.zero)
        {
            if (inputJugador.x > 0)
            {
                paDondeVeo = Direccion.Derecha;
            }
            else if (inputJugador.x < 0)
            {
                paDondeVeo = Direccion.Izquierda;
            }
            else if (inputJugador.y > 0)
            {
                paDondeVeo = Direccion.Arriba;
            }
            else
            {
                paDondeVeo = Direccion.Abajo;
            }
        }



    }


    private void LateUpdate()
    {
        if (inputJugador != Vector2.zero)
        {
            animCont.SetBool("EstaMoviendose", true);
            animCont.SetFloat("Horizontal", inputJugador.x);
            animCont.SetFloat("Vertical", inputJugador.y);
        }
        else
        {
            animCont.SetBool("EstaMoviendose", false);
        }
    }

    void Destruir()
    {
        StartCoroutine(WaitForAnimation(chamuscado));
    }

    void CambioDeVelocidad(int valorDeCambio)
    {
        if (cambiarVelocidad != null)
        {
            StopCoroutine(cambiarVelocidad);
            velocidadJugador = 250;
        }

        if (valorDeCambio > 0)
        {
            velocidadJugador = velocidadJugador * valorDeCambio;
        }
        else
        {
            velocidadJugador = velocidadJugador / (valorDeCambio*-1);
        }

        cambiarVelocidad = StartCoroutine(WaitForVelocity(valorDeCambio));
    }

    private IEnumerator WaitForAnimation(AnimationClip animation)
    {
        animCont.SetTrigger("Chamuscado");
        muerto = true;
        GameManager.Instance.MeMori(gameObject);
        yield return new WaitForSeconds(animation.length);
        Destroy(gameObject);
    }

    private IEnumerator WaitForVelocity(int valorDeCambio)
    {
        yield return new WaitForSeconds(3); 
        if (valorDeCambio > 0)
        {
            velocidadJugador = velocidadJugador / valorDeCambio;
        }
        else
        {
            velocidadJugador = velocidadJugador * (valorDeCambio * -1);
        }
    }

}

