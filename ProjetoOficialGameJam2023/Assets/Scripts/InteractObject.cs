using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class InteractObject : MonoBehaviour
{
    public int identificador;
    [SerializeField]
    private PlayerInteract playerInteract;

    [SerializeField]
    private UnityEvent botaoApertado;

    private bool canExecute;
    public string cena1;
    

    public OrganizarPastas gerarComandos;
    
    void Start()
    {
        gerarComandos = FindObjectOfType<OrganizarPastas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canExecute)
        {
            if (playerInteract.isInteracting == true)
            {
                botaoApertado.Invoke();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canExecute = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canExecute = false;
    }

    public void NextScene()
    {
        if (identificador == 0)
        {
            // gerarComandos.Fase1Check();
            SceneManager.LoadScene("Imagens");
        }
        if (identificador == 1)
        {
            //gerarComandos.Fase2Check();
             SceneManager.LoadScene("Músicas");

        }

        if (identificador == 2)
        {
            //gerarComandos.Fase3Check();
            SceneManager.LoadScene("Documentos");

        }

    }
}
