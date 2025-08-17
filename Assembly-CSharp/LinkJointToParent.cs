using System;
using UnityEngine;

public class LinkJointToParent : MonoBehaviour
{
	// Token: 0x060008C7 RID: 2247 RVA: 0x00038F94 File Offset: 0x00037194
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
