using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class LimitInstanceCount : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x00017FBC File Offset: 0x000161BC
	private static void AddInstance(LimitInstanceCount instance)
	{
		LimitInstanceCount.instances.Add(instance);
		if (LimitInstanceCount.instances.Count > 15)
		{
			Object gameObject = LimitInstanceCount.instances[0].gameObject;
			LimitInstanceCount.instances.Remove(LimitInstanceCount.instances[0]);
			Object.Destroy(gameObject);
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000023CB File Offset: 0x000005CB
	private static void RemoveInstance(LimitInstanceCount instance)
	{
		if (LimitInstanceCount.instances.Contains(instance))
		{
			LimitInstanceCount.instances.Remove(instance);
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000023E6 File Offset: 0x000005E6
	private void OnEnable()
	{
		LimitInstanceCount.AddInstance(this);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000023EE File Offset: 0x000005EE
	private void OnDisable()
	{
		LimitInstanceCount.RemoveInstance(this);
	}

	// Token: 0x0400006A RID: 106
	private static List<LimitInstanceCount> instances = new List<LimitInstanceCount>();

	// Token: 0x0400006B RID: 107
	private const int instanceLimit = 15;
}
