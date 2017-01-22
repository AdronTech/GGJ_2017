using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Barracks : AbstractBaseBuilding {

    public Vector3 effectiveRange = new Vector3(5, 20, 5);
    public float sonarPulseIntervall = 2f;
    public int ticksToLeaveAlert = 10;
    public float alertResourceDrain = -0.1f;
    public float attackAimDuration = 1f, attackFlareDuration = 0.1f;

    private LineRenderer ousiaRay;
    private Collider[] enemiesSpotted = new Collider[0];

    private bool isAlerted;
    public bool IsAlerted
    {
        set
        {
            isAlerted = value;
            ResourceManager m = FindObjectOfType<ResourceManager>();
            if (value)
            {
                if(m)m.RegisterResources(alertResourceDrain);
            }
            else
            {
                if(m)m.DeregisterResources(alertResourceDrain);
            }
        }
    }

	// Use this for initialization
	void Awake () {
        ousiaRay = GetComponent<LineRenderer>();
        ousiaRay.SetPosition(0, transform.position);
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }

    void Start()
    {
        StartCoroutine(Sonar());
    }

    private bool AlertCheck(out Collider[] cc)
    {
        cc = Physics.OverlapBox(transform.position,
                                            effectiveRange,
                                            Quaternion.identity,
                                            LayerMask.NameToLayer("Enemy"));
        return cc.Length > 0;
    }

    public Collider AquireTarget()
    {
        foreach(Collider enemy in enemiesSpotted)
        {
            if (EvaluateTarget(enemy))
            {
                return enemy;
            }
        }
        return null;
    }

    public bool EvaluateTarget(Collider enemy)
    {
        Vector3 dir = enemy.transform.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        return !Physics.Raycast(transform.position, dir, distance, LayerMask.NameToLayer("Building"));
    }

    public int clearTicks = 0;
    public IEnumerator Sonar()
    {
        while(true)
        {
            Collider[] cc;
            if(AlertCheck(out cc))
            {
                if (!isAlerted)
                {
                    StartCoroutine(Engaging());
                    IsAlerted = true;
                }
                clearTicks = 0;
            }
            else if(++clearTicks == ticksToLeaveAlert)
            {
                IsAlerted = false;
            }
            yield return new WaitForSeconds(sonarPulseIntervall);
        }
    }

    public IEnumerator Engaging()
    {
        Debug.Log("Engaged!");
        for(Collider target = AquireTarget(); target; target = AquireTarget())
        {
            Debug.Log("Aiming!");
            Debug.DrawLine(transform.position, target.transform.position, Color.cyan);
            yield return new WaitForSeconds(attackAimDuration);
            if (EvaluateTarget(target))
            {
                ousiaRay.SetPosition(1, target.transform.position);
                ousiaRay.enabled = true;
                Destroy(target.gameObject);
                yield return new WaitForSeconds(attackFlareDuration);
                ousiaRay.enabled = false;
            }
        }
        Debug.Log("No Targets!");
    }
}
