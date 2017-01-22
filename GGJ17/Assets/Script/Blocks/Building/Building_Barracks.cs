using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Barracks : AbstractBaseBuilding {

    public float EffectiveRange = 5;
    public float AlertCheckIntervall = 1f;
    public float AtackIntervall = 0.5f;
    public bool isAlerted;

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }

    void Update()
    {
        if (!isAlerted)
        {
            Collider[] cc = Physics.OverlapBox(transform.position, new Vector3(EffectiveRange, 20, EffectiveRange));
            foreach(Collider c in cc)
            {
                if(c.tag == "Enemy")
                {

                }
            }
        }
    }

    public IEnumerator OnAlert()
    {

    }
}
