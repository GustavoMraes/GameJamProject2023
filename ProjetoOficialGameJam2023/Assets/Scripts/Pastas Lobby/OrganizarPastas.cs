using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizarPastas : MonoBehaviour
{
     public PastaCheck pasta1check;
    public PastaLivre pasta1livre;
    public PastaLivre pasta2livre;
     public PastaBlocked pasta2disblock;
     public BarraVerificacao zero;
     public BarraVerificacao primeiro;


    
     public PastaCheck pasta2check;
     public PastaLivre pasta3livre;
     public PastaBlocked pasta3disblock;
     public BarraVerificacao segundo;

     public PastaCheck pasta3check;
     public BarraVerificacao ultimo;


    public playerController player;
    int nivel;





    // Start is called before the first frame update
    void Start()
    {
        //pasta1check = FindObjectOfType<PastaCheck>();


        // pasta1check = FindObjectOfType<PastaCheck>();

        //pasta2livre = FindObjectOfType<PastaLivre>();
        // pasta2disblock = FindObjectOfType<PastaBlocked>();
        //zero = FindObjectOfType<BarraVerificacao>();
        //primeiro = FindObjectOfType<BarraVerificacao>();

        // pasta2check = FindObjectOfType<PastaCheck>();
        //pasta3livre = FindObjectOfType<PastaLivre>();
        //pasta3disblock = FindObjectOfType<PastaBlocked>();
        //segundo = FindObjectOfType<BarraVerificacao>();

        // pasta3check = FindObjectOfType<PastaCheck>();
        // ultimo = FindObjectOfType<BarraVerificacao>();
        
        nivel = player.VerNivel();
        if (nivel == 1)
        {
            Fase1Check();
        }
        if (nivel == 2)
        {
            Fase1Check();
            Fase2Check();
        }
        if (nivel == 3)
        {
            Fase1Check();
            Fase2Check();
            Fase3Check();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Fase1Check ()
    {
        pasta1check.checkPasta();
        pasta1livre.finalizada();
        pasta2livre.liberar();
        pasta2disblock.liberar();
        zero.desligarBarra();
        primeiro.ligarBarra();
    }

    public void Fase2Check()
    {
        pasta2check.checkPasta();
        pasta2livre.finalizada();
        pasta3livre.liberar();
        pasta3disblock.liberar();
        primeiro.desligarBarra();
        segundo.ligarBarra();
    }

    public void Fase3Check()
    {
        pasta3livre.finalizada();
        pasta3check.checkPasta();
        segundo.desligarBarra();
        ultimo.ligarBarra();
    }



}
