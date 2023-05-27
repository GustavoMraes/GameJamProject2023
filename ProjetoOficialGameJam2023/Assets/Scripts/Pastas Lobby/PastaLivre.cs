using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaLivre : MonoBehaviour
{
    public int identificador;
    public playerController player;
    private int nivel;

    // Start is called before the first frame update
    void Start()
    {

        nivel = player.VerNivel();
        gameObject.SetActive(false);

        if (identificador == 0 && nivel == 0)
        {
            gameObject.SetActive(true);
        }
        if (identificador == 1 && nivel == 1)
        {
            gameObject.SetActive(true);
        }
        if (identificador == 2 && nivel == 2)
        {
            gameObject.SetActive(true);
        }
        if (identificador == 3 && nivel == 3)
        {
            gameObject.SetActive(true);
        }


    }   


    // Update is called once per frame


    public void liberar()
    {
        gameObject.SetActive(true);
    }
    public void finalizada()
    {
        gameObject.SetActive(false);
    }

   

    

}
