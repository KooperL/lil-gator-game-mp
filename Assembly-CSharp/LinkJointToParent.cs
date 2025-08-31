using System;
using UnityEngine;

public class LinkJointToParent : MonoBehaviour
{
	// Token: 0x0600073D RID: 1853 RVA: 0x00024330 File Offset: 0x00022530
	private void Start()
	{
		Joint component = base.GetComponent<Joint>();
		Transform transform = base.transform;
		Rigidbody component2;
		do
		{
			transform = transform.parent;
			component2 = transform.GetComponent<Rigidbody>();
		}
		while (component2 == null && transform.parent == null);
		component.connectedBody = component2;
	}
}
