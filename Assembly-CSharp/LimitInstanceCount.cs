using System;
using System.Collections.Generic;
using UnityEngine;

public class LimitInstanceCount : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x000035C4 File Offset: 0x000017C4
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

	// Token: 0x06000057 RID: 87 RVA: 0x00003615 File Offset: 0x00001815
	private static void RemoveInstance(LimitInstanceCount instance)
	{
		if (LimitInstanceCount.instances.Contains(instance))
		{
			LimitInstanceCount.instances.Remove(instance);
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003630 File Offset: 0x00001830
	private void OnEnable()
	{
		LimitInstanceCount.AddInstance(this);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003638 File Offset: 0x00001838
	private void OnDisable()
	{
		LimitInstanceCount.RemoveInstance(this);
	}

	private static List<LimitInstanceCount> instances = new List<LimitInstanceCount>();

	private const int instanceLimit = 15;
}
