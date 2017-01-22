using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private Text textbox;
    public string text;

    // Use this for initialization
    void Start()
    {
        textbox = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Speak()
    {
        int i = 0;
        textbox.text = "";
        AudioSource a = GetComponent<AudioSource>();
        a.loop = true;
        a.Play();
        while(i < text.Length)
        {
            textbox.text += text[i];
            ++i;
            yield return new WaitForSeconds(.07f);
        }
        a.Stop();
    }
}
