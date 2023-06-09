using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaidaCaverna : MonoBehaviour
{
    public FundoCaverna caverna;
    public BackgroundScroll praia;
    public FundoBlocos fundo;
    // Start is called before the first frame update
    void Start()
    {
        //caverna = FindObjectOfType<FundoCaverna>();
        praia = FindObjectOfType<BackgroundScroll>();
        fundo = FindObjectOfType<FundoBlocos>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            caverna.desligarFundo();
            praia.ligarFundo();
            fundo.ligarFundo();
        }
    }
}