using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPhysics))]
public class KingBehaviour : MonoBehaviour {

    public GameObject soldierPre;
    public int size;

    private List<Transform> possibleTargets;

    private List<MyPhysics> army;

    private MyPhysics my;
    private SeekBehaviour mySeek;

	void Awake () {
        my = GetComponent<MyPhysics>();
        mySeek = GetComponent<SeekBehaviour>();

        army = new List<MyPhysics>();
        possibleTargets = new List<Transform>();

        StartCoroutine(generateArmy());
        StartCoroutine(scanForTarget());
    }

    private void newSoldier()
    {
        GameObject soldier = Instantiate(soldierPre);
        soldier.transform.position = transform.position + Vector3.down * 0.5f;

        MyPhysics solPhysics = soldier.GetComponent<MyPhysics>();
        if (solPhysics != null)
            army.Add(solPhysics);

        SeekBehaviour seek = soldier.GetComponent<SeekBehaviour>();
        seek.target = transform;

        SeperationBehaviour sb = soldier.GetComponent<SeperationBehaviour>();
        if (sb != null)
            sb.targets = army;

        CohesionBehaviour cb = soldier.GetComponent<CohesionBehaviour>();
        if (cb != null)
            cb.targets = army;
        
    }

    IEnumerator generateArmy()
    {
        for (int i = 0; i < size; i++)
        {
            newSoldier();

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator scanForTarget()
    {
        while(true)
        {
            foreach(AbstractBaseBuilding a in FindObjectsOfType<AbstractBaseBuilding>())
            {
                Vector3 dist = a.transform.position - my.pos;

                if (!Physics.Raycast(my.pos, dist.normalized, dist.magnitude - 1))
                {
                    Debug.DrawLine(my.pos, a.transform.position, Color.red);
                    possibleTargets.Add(a.transform);
                }
                else
                {
                    Debug.DrawLine(my.pos, a.transform.position, Color.blue);
                    if (possibleTargets.Contains(a.transform)) possibleTargets.Remove(a.transform);
                }
            }

            if((mySeek.target != null && !possibleTargets.Contains(mySeek.target)) || mySeek.target == null)
            {
                if(possibleTargets.Count != 0)
                    mySeek.target = possibleTargets[0];
            }

            yield return new WaitForSeconds(1f);
        }

    }

}