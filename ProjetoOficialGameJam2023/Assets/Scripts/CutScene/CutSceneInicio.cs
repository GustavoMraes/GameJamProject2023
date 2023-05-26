using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneInicio : MonoBehaviour
    
{
    string[] textos = new string[5];
    GameObject[] imagens;
    GameObject texto;

    public Imagem img0;
    public Imagem img1;
    public Imagem img2;
    public Imagem img3;
    public Imagem img4;


    void Start()
    {
       

        
        textos[0] = "Mais um dia na Corporação Cápsula, o que poderia dar de errado?";
        textos[1] = "Todos trabalhando como deveriam, alguns papeando, mas Gael, Gael está entediado...";
        textos[2] = "Uma ideia se passa em sua cabeça, por que não baixar seu jogo favorito de forma pirateada?";
        textos[3] = "Ameaças detectadas!!!";
        textos[4] = "Uma rápida solução aparece, Nymus, o antivirus mais confiavel da empresa";

        texto = GameObject.Find("texto");
        texto.GetComponent<Text>().text = textos[0];
        StartCoroutine(rotina());

       
    }

    public IEnumerator rotina()
    {
         yield return new WaitForSeconds(3);
         texto.GetComponent<Text>().text = textos[0];
        img0.ligar();
        yield return new WaitForSeconds(5);
        texto.GetComponent<Text>().text = textos[1];
        img1.ligar();
        yield return new WaitForSeconds(5);
        texto.GetComponent<Text>().text = textos[2];
        img2.ligar();
        yield return new WaitForSeconds(5);
        texto.GetComponent<Text>().text = textos[3];
        img3.ligar();
        yield return new WaitForSeconds(5);
        texto.GetComponent<Text>().text = textos[4];
        img4.ligar();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("lobby");
        
    }


}
