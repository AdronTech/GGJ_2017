using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public List<string> speeches;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator talk()
    {
        foreach(string s in speeches)
        {
            GameObject g = new GameObject();
            g.transform.parent = this.gameObject.transform;
            DialogBox d = new DialogBox();

            yield return new WaitForSeconds(2.0f);
        }
    }
}
