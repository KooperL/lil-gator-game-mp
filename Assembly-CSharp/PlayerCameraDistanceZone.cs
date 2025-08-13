using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class PlayerCameraDistanceZone : MonoBehaviour
{
	// Token: 0x06000BDA RID: 3034 RVA: 0x000043C9 File Offset: 0x000025C9
	private void OnValidate()
	{
		base.enabled = false;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0000B21C File Offset: 0x0000941C
	private void OnEnable()
	{
		PlayerCameraDistanceZone.activeZones.Add(this);
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x0000B229 File Offset: 0x00009429
	private void OnDisable()
	{
		PlayerCameraDistanceZone.activeZones.Remove(this);
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x0000B237 File Offset: 0x00009437
	private void OnTriggerStay(Collider other)
	{
		this.lastTriggeredTime = Time.time;
		base.enabled = true;
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x0000B24B File Offset: 0x0000944B
	private void Update()
	{
		if (Time.time - this.lastTriggeredTime > 0.1f)
		{
			base.enabled = false;
		}
	}

	// Token: 0x04000EEF RID: 3823
	public static List<PlayerCameraDistanceZone> activeZones = new List<PlayerCameraDistanceZone>();

	// Token: 0x04000EF0 RID: 3824
	[Range(0f, 5f)]
	public float distanceMultiplier = 1f;

	// Token: 0x04000EF1 RID: 3825
	public int priority;

	// Token: 0x04000EF2 RID: 3826
	private float lastTriggeredTime;
}
