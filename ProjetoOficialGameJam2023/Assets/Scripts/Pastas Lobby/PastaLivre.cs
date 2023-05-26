using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaLivre : MonoBehaviour
{
    public int identificador;
   
    // Start is called before the first frame update
    void Start()
    {
        if (identificador != 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void liberar()
    {
        gameObject.SetActive(true);
    }
    public void finalizada()
    {
        gameObject.SetActive(false);
    }

    public void liberarPasta3() 
    {
        identificador = 1;
    }

    

}
