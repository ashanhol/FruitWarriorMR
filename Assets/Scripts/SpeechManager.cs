using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Start Game", () =>
        {
            // Call the OnStart method on every descendant object.
            this.BroadcastMessage("OnStart");
        });
        keywords.Add("Restart Game", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnRestart");
        });
        keywords.Add("Main Menu", () =>
        {
            // Call the OnMenu method on every descendant object.
            this.BroadcastMessage("OnMenu");
        });

        keywords.Add("Show Grid", () =>
        {
            // Call the ShowGrid method on every descendant object.
            this.BroadcastMessage("ShowGrid");
        });
        keywords.Add("Hide Grid", () =>
        {
            // Call the HideGrid method on every descendant object.
            this.BroadcastMessage("HideGrid");
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}