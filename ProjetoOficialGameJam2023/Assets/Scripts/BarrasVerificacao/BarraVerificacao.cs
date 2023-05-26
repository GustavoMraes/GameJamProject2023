using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraVerificacao : MonoBehaviour
{
    public int identificador;
    // Start is called before the first frame update
    void Start()
    {
        if (identificador == 0)
        {
            gameObject.SetActive(true);
        } else
        {

        }
            
    }

    public void ligarBarra()
    {
        gameObject.SetActive(true);

    }
    public void desligarBarra()
    {
        gameObject.SetActive(false);

    }
}
