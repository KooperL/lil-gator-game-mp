using System;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class LinkJointToParent : MonoBehaviour
{
	// Token: 0x06000887 RID: 2183 RVA: 0x00037624 File Offset: 0x00035824
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
