using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Barracks : AbstractBaseBuilding {

    public Vector3 effectiveRange = new Vector3(5, 20, 5);
    public float sonarPulseIntervall = 2f;
    public int ticksToLeaveAlert = 10;
    public float alertResourceDrain = -0.1f;
    public float atackIntervall = 0.5f;

    private LineRenderer ousiaRay;
    private Collider[] enemiesSpotted = new Collider[0];

    private bool isAlerted;
    public bool IsAlerted
    {
        set
        {
            isAlerted = value;
            if (value)
            {
                FindObjectOfType<ResourceManager>().RegisterResources(alertResourceDrain);
            }
            else
            {
                FindObjectOfType<ResourceManager>().DeregisterResources(alertResourceDrain);
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
            Vector3 dir = enemy.transform.position - transform.position;
            float distance = dir.magnitude;
            dir.Normalize();
            if (Physics.Raycast(transform.position, dir, distance))
            { continue; }
            else
            {
                ousiaRay.SetPosition(1, enemy.transform.position);
                return enemy;
            }

        }
        return null;
    }

    public IEnumerator Sonar()
    {
        int clearTicks = 0;
        while(true)
        {
            Collider[] cc;
            if(AlertCheck(out cc))
            {
                if(!isAlerted) StartCoroutine(Engaging());
                clearTicks = 0;
                IsAlerted = true;
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

            yield return null;
        }
    }
}
