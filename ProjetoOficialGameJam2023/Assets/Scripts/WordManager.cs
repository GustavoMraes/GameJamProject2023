using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    private void Start()
    {
        AddWord();
        AddWord();
        AddWord();
    }

    public void AddWord ()
    {
        Word word = new Word(WordGenerator.GetRandomWord());
        Debug.log(word.word);

        words.Add(word);
    }
}
