using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class Voltar : MonoBehaviour
{

    public playerController player;
    private int nivel;


    [SerializeField]
    private PlayerInteract playerInteract;

    [SerializeField]
    private UnityEvent botaoApertado;

    private bool canExecute;
    public string cena1;

    void Start()
    {
        player = FindObjectOfType<playerController>();
        playerInteract = FindObjectOfType<PlayerInteract>();
        cena1 = "Lobby";
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
        player.SubirNivel();
        SceneManager.LoadScene(cena1);

    }
}
