using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wobble/Brain")]
public class WobbleBrain : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06001044 RID: 4164 RVA: 0x000549E0 File Offset: 0x00052BE0
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

	// Token: 0x06001045 RID: 4165 RVA: 0x00054A48 File Offset: 0x00052C48
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

	// Token: 0x06001046 RID: 4166 RVA: 0x00002B40 File Offset: 0x00000D40
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0000DF8E File Offset: 0x0000C18E
	private void OnDisable()
	{
		this.isPaused = true;
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x00054AC0 File Offset: 0x00052CC0
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

	// Token: 0x06001049 RID: 4169 RVA: 0x00054B74 File Offset: 0x00052D74
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

	// Token: 0x0600104A RID: 4170 RVA: 0x00054BE4 File Offset: 0x00052DE4
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

	private WobbleBoneBase[] bones;

	public const WobbleBrain.UpdateInterval updateInterval = WobbleBrain.UpdateInterval.LateUpdate;

	private float lastFixedUpdateTime;

	private bool isPaused;

	private const float maxDistance = 50f;

	private Vector3 lastPosition;

	private float lastManagedUpdateTime;

	public enum UpdateInterval
	{
		LateUpdate,
		FixedUpdate
	}
}
