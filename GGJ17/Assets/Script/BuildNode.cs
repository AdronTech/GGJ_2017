using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour {

    private int orientation;

    private new BoxCollider collider;
    private new MeshRenderer renderer;
    private AbstractBuildingBlock block;
    private FixedJoint joint;

    public float jointBreakForce = 100;

    public Vector3 SpawnPosition
    {
        get
        {
            return AbstractBuildingBlock.SpawnPoint(transform.parent.position, orientation);
        }
    }

    public bool Enable
    {
        set
        {
            if (!joint)
            {
                collider.enabled = value;
                renderer.enabled = value;
            }
        }
    }

    void Awake()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<MeshRenderer>();
        collider.enabled = true;
    }

    public void Init(int i, AbstractBuildingBlock block)
    {
        this.block = block;
        orientation = i;
        transform.position = transform.parent.position;
        switch (orientation)
        {
            case AbstractBuildingBlock.up:
                transform.Translate(0, 0.5f, 0);
                break;
            case AbstractBuildingBlock.down:
                transform.Translate(0, -0.5f, 0);
                break;
            case AbstractBuildingBlock.left:
                transform.Translate(-0.5f, 0, 0);
                break;
            case AbstractBuildingBlock.right:
                transform.Translate(0.5f, 0, 0);
                break;
            case AbstractBuildingBlock.front:
                transform.Translate(0, 0, -0.5f);
                break;
            case AbstractBuildingBlock.back:
                transform.Translate(0, 0, 0.5f);
                break;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        BuildNode node = c.GetComponent<BuildNode>();
        if (node)
        {
            block.neighbors[orientation] = node.block;
            joint = block.gameObject.AddComponent<FixedJoint>();
            joint.breakForce = jointBreakForce;
            joint.connectedBody = node.block.GetComponent<Rigidbody>();
            renderer.enabled = false;
        }
    }

    void OnTriggerExit(Collider c)
    {
        BuildNode node = c.GetComponent<BuildNode>();
        if (node)
        {
            block.neighbors[orientation] = null;
            if(joint) Destroy(joint);
            renderer.enabled = true;
        }
    }
}
