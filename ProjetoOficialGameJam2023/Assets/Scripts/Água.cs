using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Água : MonoBehaviour

{
    public int dano = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerController>().TakeDamage(dano);
        }
    }
}
