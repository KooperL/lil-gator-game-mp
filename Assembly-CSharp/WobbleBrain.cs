using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wobble/Brain")]
public class WobbleBrain : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06001045 RID: 4165 RVA: 0x00054CA8 File Offset: 0x00052EA8
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

	// Token: 0x06001046 RID: 4166 RVA: 0x00054D10 File Offset: 0x00052F10
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

	// Token: 0x06001047 RID: 4167 RVA: 0x00002B40 File Offset: 0x00000D40
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0000DF98 File Offset: 0x0000C198
	private void OnDisable()
	{
		this.isPaused = true;
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x00054D88 File Offset: 0x00052F88
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

	// Token: 0x0600104A RID: 4170 RVA: 0x00054E3C File Offset: 0x0005303C
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

	// Token: 0x0600104B RID: 4171 RVA: 0x00054EAC File Offset: 0x000530AC
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
