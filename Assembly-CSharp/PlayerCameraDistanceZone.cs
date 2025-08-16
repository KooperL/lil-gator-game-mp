using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraDistanceZone : MonoBehaviour
{
	// Token: 0x06000C26 RID: 3110 RVA: 0x000044B5 File Offset: 0x000026B5
	private void OnValidate()
	{
		base.enabled = false;
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0000B50F File Offset: 0x0000970F
	private void OnEnable()
	{
		PlayerCameraDistanceZone.activeZones.Add(this);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0000B51C File Offset: 0x0000971C
	private void OnDisable()
	{
		PlayerCameraDistanceZone.activeZones.Remove(this);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0000B52A File Offset: 0x0000972A
	private void OnTriggerStay(Collider other)
	{
		this.lastTriggeredTime = Time.time;
		base.enabled = true;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0000B53E File Offset: 0x0000973E
	private void Update()
	{
		if (Time.time - this.lastTriggeredTime > 0.1f)
		{
			base.enabled = false;
		}
	}

	public static List<PlayerCameraDistanceZone> activeZones = new List<PlayerCameraDistanceZone>();

	[Range(0f, 5f)]
	public float distanceMultiplier = 1f;

	public int priority;

	private float lastTriggeredTime;
}
