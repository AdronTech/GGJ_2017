using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public List<string> speeches;
    public GameObject dialogLeft, dialogRight;

    // Use this for initialization
    void Start()
    {
        IEnumerator t = Talk();
        StartCoroutine(t);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Talk()
    {
        for(int i = 0; i < speeches.Count; ++i)
        {
            GameObject box = null;
            if(i%2 == 0)
            {
                box = Instantiate(dialogLeft);
            }
            else
            {
                box = Instantiate(dialogRight);
            }

            Canvas canvas = FindObjectOfType<Canvas>();

            box.transform.parent = canvas.transform;

            RectTransform rect = box.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector3(rect.rect.width / 2 + 17,
                -(canvas.pixelRect.height + rect.rect.height / 2));

            yield return StartCoroutine(MoveDialogBox(rect, 
                new Vector3(rect.anchoredPosition.x, 
                -rect.rect.height / 2 - (i * rect.rect.height) - 14)));

            yield return StartCoroutine(box.GetComponent<DialogBox>().Speak(speeches[i]));
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


}
