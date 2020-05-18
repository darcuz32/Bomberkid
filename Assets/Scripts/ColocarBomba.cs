﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarBomba : MonoBehaviour
{

    float damage;
    [SerializeField]
    GameObject bomba;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    float tiempoDeDetonacion;
    [SerializeField]
    int modificadorDamage;
    [SerializeField]
    int alcance;
    Transform offset;
    string jugadorAsignado;

    MovimientoJugador movJ;

    void Awake()
    {
        movJ = GetComponent<MovimientoJugador>();
        offset = transform.GetChild(0);
        jugadorAsignado = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jugadorAsignado + "Fire1") && !movJ.EstaMuerto)
        {
            GameObject bombaADetonar = Instantiate(bomba, offset.position, new Quaternion());
            bombaADetonar.GetComponent<Bomba>().Init(tiempoDeDetonacion, modificadorDamage, jugadorAsignado, alcance, explosion);
        }
    }
}