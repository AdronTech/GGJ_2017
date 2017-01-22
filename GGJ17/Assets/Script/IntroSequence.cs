using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{

    public GameObject dialog;
    private Dialog d;
    public string nextSceneName;

    // Use this for initialization
    void Start()
    {
        d = dialog.GetComponent<Dialog>();
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
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

        d.Reset();

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "Mission control, we are now entering Psion's orbit.";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "Mission control, please come in! We are just above Psion..... Do you copy?";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "They are not answering!";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "Check the main systems!";
        d.speeches.Add(e);

        e = new Dialog.DialogElement();
        e.dialogBox = d.dialogBoxes[1];
        e.text = "Whate the ...? Something has fried our computer! We are coming down hard. Brace for impact.";
        d.speeches.Add(e);

        yield return StartCoroutine(d.Talk());

        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
