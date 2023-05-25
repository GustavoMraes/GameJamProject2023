using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cura : MonoBehaviour
{

    public playerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        player.Curar(3);
    }

}
