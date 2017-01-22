using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseBuilding : AbstractBuildingBlock {

    [SerializeField]
    private float hp = 10;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp < 0)
            {
                DestroyBuilding();
            }
        }
    }

    public void DestroyBuilding()
    {

        Destroy(gameObject);
    }
}
