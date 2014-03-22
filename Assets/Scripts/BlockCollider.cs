using UnityEngine;
using System.Collections;

public class BlockCollider : MonoBehaviour 
{
    public int blockIndex;
    public bool BlockIsRightPlace { get; set; }

    void Start()
    {
        this.blockIndex = int.Parse(name.Replace("Collider", ""));
        this.BlockIsRightPlace = true;
    }

    void OnDrawGizmos()
    {
        if (BlockIsRightPlace)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.25f);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (blockIndex.ToString().Equals(hit.name.Replace("Block","")))
            BlockIsRightPlace = true;
        else
            BlockIsRightPlace = false;
    }
}
