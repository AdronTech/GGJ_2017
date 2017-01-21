using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPhysics))]
public class KingBehaviour : MonoBehaviour {

    public GameObject soldierPre;
    public int size;

    private List<MyPhysics> army;

    private MyPhysics my;

	void Awake () {
        my = GetComponent<MyPhysics>();
        army = new List<MyPhysics>();

        StartCoroutine(generateArmy());

    }

    private void newSoldier()
    {
        GameObject soldier = Instantiate(soldierPre);
        soldier.transform.position = transform.position + Vector3.down * 0.5f;

        MyPhysics solPhysics = soldier.GetComponent<MyPhysics>();
        if (solPhysics != null)
            army.Add(solPhysics);

        SeekBehaviour seek = soldier.GetComponent<SeekBehaviour>();
        seek.target = my;

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
	
}
