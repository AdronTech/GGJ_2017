using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AttackBehaviour : MonoBehaviour {

    public Transform target;
    LineRenderer lr;

    bool canSee;

    public float delay;
    public float damage;

	void Start () {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

        StartCoroutine(inRange());
        StartCoroutine(attack());
	}

    public void startAttack(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        lr.enabled = target != null;

        if (lr.enabled)
        {
            Vector3[] positions = new Vector3[2];

            positions[0] = transform.position;
            positions[1] = target.position;

            lr.SetPositions(positions);
        }
    }

    public void stopAttack()
    {
        target = null;
    }

    IEnumerator inRange()
    {
        while(true)
        {
            if (target)
            {
                Vector3 dist = target.position - transform.position;

                canSee = !Physics.Raycast(transform.position, dist.normalized, dist.magnitude - 1);
                lr.enabled = canSee;
            }
            else
            {
                canSee = false;
            }
                      
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator attack()
    {
        while(true)
        {
            if(target && canSee)
            {
                AbstractBaseBuilding t = target.GetComponent<AbstractBaseBuilding>();
                if(t)
                {
                    t.HP -= damage;
                }

                yield return new WaitForSeconds(delay);
            }

            yield return null;
        }

    }
}
