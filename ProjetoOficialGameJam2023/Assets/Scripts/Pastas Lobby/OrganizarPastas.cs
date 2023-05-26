using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizarPastas : MonoBehaviour
{
     public PastaCheck pasta1check;
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





    // Start is called before the first frame update
    void Start()
    {
        pasta1check = FindObjectOfType<PastaCheck>();
        pasta2livre = FindObjectOfType<PastaLivre>();
        zero = FindObjectOfType<BarraVerificacao>();
        primeiro = FindObjectOfType<BarraVerificacao>();

        pasta2check = FindObjectOfType<PastaCheck>();
        pasta3livre = FindObjectOfType<PastaLivre>();
        pasta3disblock = FindObjectOfType<PastaBlocked>();
        segundo = FindObjectOfType<BarraVerificacao>();

        pasta3check = FindObjectOfType<PastaCheck>();
        ultimo = FindObjectOfType<BarraVerificacao>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Fase1Check ()
    {
        pasta1check.checkPasta();
        pasta2livre.liberar();
        zero.desligarBarra();
        primeiro.ligarBarra();
    }

    public void Fase2Check()
    {
        pasta2check.checkPasta();
        pasta3livre.liberar();
        primeiro.desligarBarra();
        segundo.ligarBarra();
    }

    public void Fase3Check()
    {
        pasta3check.checkPasta();
        segundo.desligarBarra();
        ultimo.ligarBarra();
    }



}
