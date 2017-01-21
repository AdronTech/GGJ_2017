using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private Text textbox;

    // Use this for initialization
    void Start()
    {
        textbox = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Speak(string text)
    {
        int i = 0;
        textbox.text = "";

        while(i < text.Length)
        {
            textbox.text += text[i];
            ++i;
            yield return new WaitForSeconds(.5f);
        }
    }
}
