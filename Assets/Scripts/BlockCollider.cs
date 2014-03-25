using UnityEngine;
using System.Collections;

public class BlockCollider : MonoBehaviour
{
    #region Fields
    private int blockIndex;
    public bool BlockIsRightPlace { get; set; }
    #endregion

    #region Start
    void Start()
    {
        this.blockIndex = int.Parse(name.Replace("Collider", ""));
        this.BlockIsRightPlace = true;
    }
    #endregion

    #region Draw Gizmos
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (BlockIsRightPlace)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.25f);
    }
#endif
    #endregion

    #region Trigger Enter
    void OnTriggerEnter(Collider hit)
    {
        if (blockIndex.ToString().Equals(hit.name.Replace("Block","")))
            BlockIsRightPlace = true;
        else
            BlockIsRightPlace = false;
    }
    #endregion
}
