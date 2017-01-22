﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{

    public GameObject dialog;
    private Dialog d;
    // Use this for initialization
    void Start()
    {
        d = dialog.GetComponent<Dialog>();
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator PlayIntro()
    {
        d.speeches = new List<Dialog.DialogElement>();

        Dialog.DialogElement e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[0];
        e.text = "Waverider, please come in. This is Mission Control.";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "Mission Control, this is the Waverider! What are your orders?";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[0];
        e.text = "We have an important mission for you. You have to travel to a near solar system where you will find the planet of Psion. Our research team has located a powerful new energy source below the planet's surface.";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[0];
        e.text = "Your mission will be to retrieve a probe from this new energy form and bring it back to Earth! This is a mission of the highest priority. I am sending the coordinates to your computer as we speak. Good luck Waverider!";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "Roger mission control! We will not let you down! Over.";
        d.speeches.Add(e);

        yield return StartCoroutine(d.Talk());
    }
}