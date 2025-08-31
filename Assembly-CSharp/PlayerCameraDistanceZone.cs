using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraDistanceZone : MonoBehaviour
{
	// Token: 0x06000A25 RID: 2597 RVA: 0x0002F4F0 File Offset: 0x0002D6F0
	private void OnValidate()
	{
		base.enabled = false;
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0002F4F9 File Offset: 0x0002D6F9
	private void OnEnable()
	{
		PlayerCameraDistanceZone.activeZones.Add(this);
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0002F506 File Offset: 0x0002D706
	private void OnDisable()
	{
		PlayerCameraDistanceZone.activeZones.Remove(this);
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0002F514 File Offset: 0x0002D714
	private void OnTriggerStay(Collider other)
	{
		this.lastTriggeredTime = Time.time;
		base.enabled = true;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0002F528 File Offset: 0x0002D728
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
