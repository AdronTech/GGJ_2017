using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour {
    #region references
    private new BoxCollider collider;
    private new MeshRenderer renderer;
    private AbstractBuildingBlock block;
    #endregion

    private int orientation;
    private FixedJoint joint;
    public float jointBreakForce = 100;

    void Awake()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<MeshRenderer>();
        collider.enabled = true;
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
        }
    }
    
    public void Init(int i, AbstractBuildingBlock block, bool invis)
    {
        if (invis)
        {
            Destroy(renderer);
            Destroy(GetComponent<MeshFilter>());
        }
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

    public void Show()
    {
        if (!joint)
        {
            if (renderer) renderer.enabled = true;
            collider.enabled = true;
        }
    }

    public void Hide()
    {
        if (renderer) renderer.enabled = false;
        collider.enabled = false;
    }

    public Vector3 GetSpawnPosition()
    {
        return AbstractBuildingBlock.SpawnPoint(transform.parent.position, orientation);
    }
}
