using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarDialogo : MonoBehaviour
{
    public GameObject caixaDeDialogo;
    public playerController player_Controller;
    private bool passar_Dialogo = false;

    void Start()
    {
        caixaDeDialogo.SetActive(false);
    }

    void Update()
    {
        if(passar_Dialogo == true)
        {
            passarDialogo();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AtivarCaixaDialogo();
        player_Controller.SetCanMove(false);
        passar_Dialogo = true;
    }

    private void passarDialogo()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            DesativarCaixaDialogo();
            passar_Dialogo = false;
            player_Controller.SetCanMove(true);
        }
    }

    private void AtivarCaixaDialogo()
    {
        caixaDeDialogo.SetActive(true);
    }

    private void DesativarCaixaDialogo()
    {
        caixaDeDialogo.SetActive(false);
    }
}
