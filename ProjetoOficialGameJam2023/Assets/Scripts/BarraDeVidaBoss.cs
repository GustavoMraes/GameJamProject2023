using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaBoss : MonoBehaviour
{
    public float health = 50;
    public float healthMax = 50;

    public Image healthBar;
    public BossHealth pegarVida;


    // Start is called before the first frame update
    void Start()
    {
        pegarVida = FindObjectOfType<BossHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        health = pegarVida.quantodevida();        
        healthBar.fillAmount = health / healthMax;
    }
    
}
