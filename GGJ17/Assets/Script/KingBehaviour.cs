﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPhysics))]
public class KingBehaviour : MonoBehaviour {

    public GameObject soldierPre;
    public int size;

    private List<MyPhysics> army;

    private MyPhysics my;
    private SeekBehaviour mySeek;

    public float attackRange;

    public KingState state;

	void Awake () {
        my = GetComponent<MyPhysics>();
        mySeek = GetComponent<SeekBehaviour>();

        army = new List<MyPhysics>();

        state = KingState.Seeking;

        StartCoroutine(generateArmy());
        StartCoroutine(stateMachineManager());
        StartCoroutine(scanForTarget());
        StartCoroutine(attackTarget());
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

    public void Update()
    {
        if (mySeek.target)
            Debug.DrawLine(my.pos, mySeek.target.position);
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
        while (true)
        {
            yield return new WaitUntil(() => state == KingState.Seeking);

            List<Transform> possibleTargets = new List<Transform>();

            foreach (AbstractBaseBuilding a in FindObjectsOfType<AbstractBaseBuilding>())
            {
                if (a.neighbors[AbstractBuildingBlock.down] && a.neighbors[AbstractBuildingBlock.down].tag == "Ground")
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
                    }
                }
            }

            Transform bestTarget = null;

            foreach (Transform possible in possibleTargets)
            {
                if (!bestTarget)
                {
                    bestTarget = possible;
                    continue;
                }

                if (getPriority(possible) > getPriority(bestTarget) ||
                    (getPriority(possible) == getPriority(bestTarget) && Vector3.Distance(my.pos, possible.position) <= Vector3.Distance(my.pos, bestTarget.position)))
                    bestTarget = possible;
            }

            if (!mySeek.target || 
                !possibleTargets.Contains(mySeek.target) ||
                getPriority(bestTarget) > getPriority(mySeek.target) ||
                (getPriority(bestTarget) == getPriority(mySeek.target) && Vector3.Distance(my.pos, bestTarget.position) <= Vector3.Distance(my.pos, mySeek.target.position)))
                mySeek.target = bestTarget;

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator attackTarget()
    {
        while (true)
        {
            yield return new WaitUntil(() => state == KingState.Attack);

            Debug.Log("Attack " + mySeek.target);
        }
    }

    IEnumerator stateMachineManager()
    {
        while(true)
        {
            switch(state)
            {
                case KingState.Seeking:
                    if (!mySeek.target) break;

                    if (Mathf.Abs(my.pos.x - mySeek.target.position.x) <= attackRange && Mathf.Abs(my.pos.z - mySeek.target.position.z) <= attackRange)
                    { 
                        state = KingState.Attack;
                        mySeek.stop();

                        foreach(MyPhysics soldier in army)
                        {
                            if (soldier)
                            {
                                AttackBehaviour ah = soldier.GetComponent<AttackBehaviour>();
                                if (ah) ah.startAttack(mySeek.target);
                            }
                        }

                    }

                    break;
                case KingState.Attack:
                    if (!mySeek.target)
                    {
                        foreach (MyPhysics soldier in army)
                        {
                            AttackBehaviour ah = soldier.GetComponent<AttackBehaviour>();
                            if (ah) ah.stopAttack();
                        }
                        mySeek.start();
                        state = KingState.Seeking;
                        Debug.Log("Destroyed");
                    }

                    break;
            }

            yield return null;
        }
    }

    public int getPriority(Transform block)
    {
        if (!block) return 0;

        AbstractBaseBuilding abb = block.GetComponent<AbstractBaseBuilding>();

        if (abb)
        { 
            if (abb is Building_Flag)
                return 4;
            if (abb is Building_Barracks)
                return 3;
            if (abb is Building_Extractor)
                return 2;
            if (abb is Building_Wall)
                return 1;
        }

        return 0;
    }

    public enum KingState
    {
        Seeking,
        Attack,
    }

}