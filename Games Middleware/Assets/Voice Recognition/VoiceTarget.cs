using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceTarget : MonoBehaviour
{
    [SerializeField]
    string command;
    bool playerAt;

    private KeywordRecognizer keyWordRec;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private VoiceMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<VoiceMovement>().GetComponent<VoiceMovement>();

        actions.Add(command, WalkTo);

        playerAt = false;

        keyWordRec = new KeywordRecognizer(actions.Keys.ToArray());
        keyWordRec.OnPhraseRecognized += RecognisedSpeech;
        keyWordRec.Start();
    }

    private void RecognisedSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    private void WalkTo()
    {
        if(!playerAt)
        {
            Vector3 target = (transform.position - player.transform.position).normalized;
            player.WalkTowards(new Vector3(target.x, player.transform.position.y, target.z));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerAt = true;
        player.Stop();
    }

    private void OnCollisionExit(Collision collision)
    {
        playerAt = false;
    }
}
