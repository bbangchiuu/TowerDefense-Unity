using Core.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextNodeLocation : MonoBehaviour
{
    [SerializeField]
    List<NodeLocation> linkedNodes;

    int nodeIndex;

    public NodeLocation GetNextNode()
    {
        if (linkedNodes.Next(ref nodeIndex, true))
        {
            return linkedNodes[nodeIndex];
        }
        return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (linkedNodes == null)
        {
            return;
        }
        int count = linkedNodes.Count;
        for (int i = 0; i < count; i++)
        {
            NodeLocation node = linkedNodes[i];
            if (node != null)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
#endif
}
