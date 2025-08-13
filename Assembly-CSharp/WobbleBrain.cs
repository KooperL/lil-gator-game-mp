using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000264 RID: 612
[AddComponentMenu("Wobble/Brain")]
public class WobbleBrain : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000D3B RID: 3387 RVA: 0x00040350 File Offset: 0x0003E550
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

	// Token: 0x06000D3C RID: 3388 RVA: 0x000403B8 File Offset: 0x0003E5B8
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

	// Token: 0x06000D3D RID: 3389 RVA: 0x00040430 File Offset: 0x0003E630
	private void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0004043D File Offset: 0x0003E63D
	private void OnDisable()
	{
		this.isPaused = true;
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x00040454 File Offset: 0x0003E654
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

	// Token: 0x06000D40 RID: 3392 RVA: 0x00040508 File Offset: 0x0003E708
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

	// Token: 0x06000D41 RID: 3393 RVA: 0x00040578 File Offset: 0x0003E778
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

	// Token: 0x04001182 RID: 4482
	private WobbleBoneBase[] bones;

	// Token: 0x04001183 RID: 4483
	public const WobbleBrain.UpdateInterval updateInterval = WobbleBrain.UpdateInterval.LateUpdate;

	// Token: 0x04001184 RID: 4484
	private float lastFixedUpdateTime;

	// Token: 0x04001185 RID: 4485
	private bool isPaused;

	// Token: 0x04001186 RID: 4486
	private const float maxDistance = 50f;

	// Token: 0x04001187 RID: 4487
	private Vector3 lastPosition;

	// Token: 0x04001188 RID: 4488
	private float lastManagedUpdateTime;

	// Token: 0x02000425 RID: 1061
	public enum UpdateInterval
	{
		// Token: 0x04001D58 RID: 7512
		LateUpdate,
		// Token: 0x04001D59 RID: 7513
		FixedUpdate
	}
}
