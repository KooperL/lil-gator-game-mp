using System;
using System.Collections.Generic;
using UnityEngine;

public class LimitInstanceCount : MonoBehaviour
{
	// Token: 0x06000057 RID: 87 RVA: 0x000187F8 File Offset: 0x000169F8
	private static void AddInstance(LimitInstanceCount instance)
	{
		LimitInstanceCount.instances.Add(instance);
		if (LimitInstanceCount.instances.Count > 15)
		{
			global::UnityEngine.Object gameObject = LimitInstanceCount.instances[0].gameObject;
			LimitInstanceCount.instances.Remove(LimitInstanceCount.instances[0]);
			global::UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000242F File Offset: 0x0000062F
	private static void RemoveInstance(LimitInstanceCount instance)
	{
		if (LimitInstanceCount.instances.Contains(instance))
		{
			LimitInstanceCount.instances.Remove(instance);
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000244A File Offset: 0x0000064A
	private void OnEnable()
	{
		LimitInstanceCount.AddInstance(this);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002452 File Offset: 0x00000652
	private void OnDisable()
	{
		LimitInstanceCount.RemoveInstance(this);
	}

	private static List<LimitInstanceCount> instances = new List<LimitInstanceCount>();

	private const int instanceLimit = 15;
}
