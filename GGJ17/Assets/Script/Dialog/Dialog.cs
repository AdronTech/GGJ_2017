﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public List<GameObject> dialogBoxes;

    public struct DialogElement
    {
        public GameObject dialogBox;
        public string text;
    }

    public List<DialogElement> speeches;

    // Use this for initialization
    void Start()
    {
        //IEnumerator t = Talk();
        //StartCoroutine(t);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Talk()
    {
        for(int i = 0; i < speeches.Count; ++i)
        {
            GameObject box = Instantiate(speeches[i].dialogBox);
            Canvas canvas = FindObjectOfType<Canvas>();

            if(canvas == null)
            {
                throw new System.Exception("No Canvas in the Scene");
            }

            box.transform.parent = canvas.transform;
            RectTransform rect = box.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector3(rect.rect.width / 2 + 17,
                -(canvas.pixelRect.height + rect.rect.height / 2));

            yield return StartCoroutine(MoveDialogBox(rect, 
                new Vector3(rect.anchoredPosition.x, 
                -rect.rect.height / 2 - (i * rect.rect.height) - 30)));

            rect.anchoredPosition = new Vector3(rect.rect.width / 2 + 17,
                -rect.rect.height / 2 - (i * rect.rect.height) - 30 - 15*i);

            DialogBox d = box.GetComponent<DialogBox>();
            d.text = speeches[i].text;
            yield return StartCoroutine(d.Speak());
        }
    }

    public IEnumerator MoveDialogBox(RectTransform rect, Vector3 target)
    {
        while(rect.anchoredPosition.y <= target.y)
        {
            rect.anchoredPosition += Vector2.up * 1000 * Time.deltaTime;
            yield return null;
        }
    }

    public void Reset()
    {
        foreach(DialogBox d in FindObjectsOfType<DialogBox>())
        {
            Destroy(d.gameObject);
        }

        speeches.Clear();
    }

    internal void Clear()
    {
        speeches.Clear();
    }


}
