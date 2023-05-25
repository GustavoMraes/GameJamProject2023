using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private static string[] wordList = { "Abrobora", "Bergamota", "Catarina", "Doco", "Eduard", "Felix", "Gay", "Humberto", 
                                         "Ir�s", "Jo�a", "Kaio", "Le�o", "Monica", "Nunca", "Octavian", "Pergunta", "Queijo", 
                                        "Resposta", "Sapo", "Tatiane", "Urubu", "Vitoria", "Walle", "Xana", "Yan", "Zoom"  };
    
    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        string randomWord = wordlist[randomIndex];

        return randomWord;
    }
}
