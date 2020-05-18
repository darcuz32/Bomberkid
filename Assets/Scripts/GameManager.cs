using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;
    [SerializeField]
    Text winMessage;

    private void Start()
    {
        winMessage.text = "";
    }

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    public void MeMori(GameObject muerto)
    {
        if (muerto == player1)
        {
            winMessage.text = "Win player 2";
        }
        else
        {
            winMessage.text = "Win player 1";
        }
    }
}
