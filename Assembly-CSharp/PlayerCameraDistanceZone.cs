using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraDistanceZone : MonoBehaviour
{
	// Token: 0x06000C27 RID: 3111 RVA: 0x000044B5 File Offset: 0x000026B5
	private void OnValidate()
	{
		base.enabled = false;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0000B52E File Offset: 0x0000972E
	private void OnEnable()
	{
		PlayerCameraDistanceZone.activeZones.Add(this);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0000B53B File Offset: 0x0000973B
	private void OnDisable()
	{
		PlayerCameraDistanceZone.activeZones.Remove(this);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0000B549 File Offset: 0x00009749
	private void OnTriggerStay(Collider other)
	{
		this.lastTriggeredTime = Time.time;
		base.enabled = true;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0000B55D File Offset: 0x0000975D
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
