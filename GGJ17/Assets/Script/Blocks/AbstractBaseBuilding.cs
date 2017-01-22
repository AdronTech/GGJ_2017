using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseBuilding : AbstractBuildingBlock {

    [SerializeField]
    private float hp = 10;
    public float HP
    {
        set
        {
            hp = value;
            if (hp < value)
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
