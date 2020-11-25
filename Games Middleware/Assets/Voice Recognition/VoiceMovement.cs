using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keyWordRec;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private Animator anim;

    private Vector3 movement;

    private void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("left", Left);
        actions.Add("back", Back);
        actions.Add("right", Right);
        actions.Add("stop", Stop);
        actions.Add("punch", Punch);

        keyWordRec = new KeywordRecognizer(actions.Keys.ToArray());
        keyWordRec.OnPhraseRecognized += RecognisedSpeech;
        keyWordRec.Start();

        anim = GetComponent<Animator>();
        movement = Vector3.zero;
    }

    private void Update()
    {
        transform.position += movement;
    }

    private void RecognisedSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        anim.SetBool("isWalking", true);
        movement = transform.forward * Time.deltaTime;
    }

    private void Back()
    {
        anim.SetBool("isWalking", true);
        movement = -transform.forward * Time.deltaTime;
    }

    private void Left()
    {
        anim.SetBool("isWalking", true);
        movement = -transform.right * Time.deltaTime;
    }

    private void Right()
    {
        anim.SetBool("isWalking", true);
        movement = transform.right * Time.deltaTime;
    }

    public void Stop()
    {
        anim.SetBool("isWalking", false);
        movement = Vector3.zero;
    }

    private void Punch()
    {
        anim.SetTrigger("Attack");
    }

    public void WalkTowards(Vector3 target)
    {
        anim.SetBool("isWalking", true);
        movement = target * Time.deltaTime;
    }
}
