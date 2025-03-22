using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
using UnityEngine.Windows.Speech;
#endif


public class GameOverCommands : MonoBehaviour
{
    #if UNITY_STANDALONE_WIN || UNITY_EDITOR
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        actions.Add("restart", RestartButton);
        actions.Add("exit", ExitButton);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void RestartButton()
    {
        SceneManager.LoadScene("RunnerGame");
    }

    private void ExitButton()
    {
        Application.Quit();
    }
    #endif
}
