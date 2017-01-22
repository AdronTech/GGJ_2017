using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Barracks : AbstractBaseBuilding {

    public float effectiveRange = 5;
    public float sonarPulseIntervall = 2f;
    public int ticksToLeaveAlert = 10;
    public float alertResourceDrain = -0.1f;
    public float attackAimDuration = 1f, attackFlareDuration = 0.1f;

    public Collider[] enemiesSpotted = new Collider[0];
    private LineRenderer ousiaRay;

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
        int layer = 1 << LayerMask.NameToLayer("Enemy");

        cc = Physics.OverlapSphere(Vector3.Scale(transform.position, new Vector3(1, 0, 1)), effectiveRange, layer);

        return cc.Length > 0;
    }

    public Collider AquireTarget()
    {
        foreach(Collider enemy in enemiesSpotted)
        {
            if (enemy)
            {
                Debug.DrawLine(transform.position, enemy.transform.position, Color.cyan, 1);
                if (EvaluateTarget(enemy))
                {
                    return enemy;
                }
            }
        }
        return null;
    }

    public bool EvaluateTarget(Collider enemy)
    {
        if (enemy)
        {
            int layer = 1 << LayerMask.NameToLayer("Building");
            Vector3 dir = enemy.transform.position - transform.position;
            float distance = dir.magnitude;
            dir.Normalize();
            Debug.DrawRay(transform.position + dir * (Vector3.one * 0.5f).magnitude, dir * distance, Color.red, 1);
            return !Physics.Raycast(transform.position + dir * (Vector3.one*0.5f).magnitude, dir, distance, layer);
        }
        return false;
    }

    public int clearTicks = 0;
    public IEnumerator Sonar()
    {
        while(true)
        {
            if(AlertCheck(out enemiesSpotted))
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
        for(Collider target = AquireTarget(); target; target = AquireTarget())
        {
            yield return new WaitForSeconds(attackAimDuration);
            if (EvaluateTarget(target))
            {
                ousiaRay.SetPosition(1, target.transform.position);
                ousiaRay.enabled = true;
                Destroy(target.gameObject, attackFlareDuration);
                yield return new WaitForSeconds(attackFlareDuration);
                ousiaRay.enabled = false;
            }
        }
    }
}
