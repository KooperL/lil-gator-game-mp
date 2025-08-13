using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200032B RID: 811
[AddComponentMenu("Wobble/Brain")]
public class WobbleBrain : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000FE9 RID: 4073 RVA: 0x00052ABC File Offset: 0x00050CBC
	private void Start()
	{
		List<WobbleBoneBase> list = new List<WobbleBoneBase>();
		this.AddChildren(base.transform, ref list);
		this.bones = list.ToArray();
		this.lastFixedUpdateTime = Time.time;
		this.lastPosition = base.transform.position;
		WobbleBoneBase[] array = this.bones;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Initialize();
		}
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x00052B24 File Offset: 0x00050D24
	private void AddChildren(Transform parent, ref List<WobbleBoneBase> boneList)
	{
		foreach (object obj in parent)
		{
			Transform transform = (Transform)obj;
			WobbleBoneBase component = transform.GetComponent<WobbleBoneBase>();
			if (component != null)
			{
				boneList.Add(component);
			}
			if (transform.childCount > 0)
			{
				this.AddChildren(transform, ref boneList);
			}
		}
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00002ADC File Offset: 0x00000CDC
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0000DC25 File Offset: 0x0000BE25
	private void OnDisable()
	{
		this.isPaused = true;
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x00052B9C File Offset: 0x00050D9C
	public void ManagedUpdate()
	{
		float num = Mathf.Min(Time.time - this.lastManagedUpdateTime, 0.5f);
		if (Vector3.Distance(base.transform.position, MainCamera.t.position) > 50f)
		{
			this.isPaused = true;
			return;
		}
		Vector3 position = base.transform.position;
		if (this.isPaused || Vector3.Distance(this.lastPosition, position) > 20f * num)
		{
			if (this.bones != null)
			{
				WobbleBoneBase[] array = this.bones;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Reacclimate();
				}
			}
			this.isPaused = false;
		}
		this.lastPosition = position;
		this.lastManagedUpdateTime = Time.time;
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x00052C50 File Offset: 0x00050E50
	private void LateUpdate()
	{
		if (this.isPaused)
		{
			return;
		}
		if (base.transform.position.IsNaN())
		{
			return;
		}
		for (int i = this.bones.Length - 1; i >= 0; i--)
		{
			this.bones[i].RunWobbleUpdate();
		}
		foreach (WobbleBoneBase wobbleBoneBase in this.bones)
		{
			wobbleBoneBase.ApplyPosition();
			wobbleBoneBase.ApplyRotation();
		}
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x00052CC0 File Offset: 0x00050EC0
	public void Reacclimate()
	{
		if (this.bones != null)
		{
			WobbleBoneBase[] array = this.bones;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Reacclimate();
			}
		}
		this.isPaused = false;
	}

	// Token: 0x040014A5 RID: 5285
	private WobbleBoneBase[] bones;

	// Token: 0x040014A6 RID: 5286
	public const WobbleBrain.UpdateInterval updateInterval = WobbleBrain.UpdateInterval.LateUpdate;

	// Token: 0x040014A7 RID: 5287
	private float lastFixedUpdateTime;

	// Token: 0x040014A8 RID: 5288
	private bool isPaused;

	// Token: 0x040014A9 RID: 5289
	private const float maxDistance = 50f;

	// Token: 0x040014AA RID: 5290
	private Vector3 lastPosition;

	// Token: 0x040014AB RID: 5291
	private float lastManagedUpdateTime;

	// Token: 0x0200032C RID: 812
	public enum UpdateInterval
	{
		// Token: 0x040014AD RID: 5293
		LateUpdate,
		// Token: 0x040014AE RID: 5294
		FixedUpdate
	}
}
