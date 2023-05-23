using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string cena;
    public GameObject optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        optionsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(cena);
    }

    public void QuitGame()
    {
        // Editor Unity (Lembrar de botar como comentario quando compilar o jogo)
        UnityEditor.EditorApplication.isPlaying = false;

        // Editor Unity (Lembrar de tirar de comentario quando compilar o jogo)
        //Application.Quit();
    }
}
