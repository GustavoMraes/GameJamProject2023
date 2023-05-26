using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraVerificacao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
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
