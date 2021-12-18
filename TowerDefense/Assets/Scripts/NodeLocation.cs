using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLocation : MonoBehaviour
{
	public NodeLocation GetNextNode()
	{
		var selector = GetComponent<NextNodeLocation>();

		if (selector != null)
		{
			return selector.GetNextNode();
		}
		return null;
	}
}
