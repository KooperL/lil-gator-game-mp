using System;
using UnityEngine;

public class LinkJointToParent : MonoBehaviour
{
	// Token: 0x060008C8 RID: 2248 RVA: 0x0003925C File Offset: 0x0003745C
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
