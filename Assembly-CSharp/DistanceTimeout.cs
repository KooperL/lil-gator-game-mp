using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class DistanceTimeout : MonoBehaviour, IManagedUpdate
{
	// Token: 0x17000099 RID: 153
	// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00006255 File Offset: 0x00004455
	public bool AutomaticTimeout
	{
		set
		{
			this.automaticTimeout = value;
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0000625E File Offset: 0x0000445E
	private void Awake()
	{
		this.sqrDistance = this.distance * this.distance;
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00006273 File Offset: 0x00004473
	private void OnEnable()
	{
		if (this.automaticTimeout)
		{
			this.automaticTimeoutTime = Time.time + this.automaticTimeoutDelay;
		}
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0000266A File Offset: 0x0000086A
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0002EE14 File Offset: 0x0002D014
	public void ManagedUpdate()
	{
		Vector3 vector = MainCamera.t.position - base.transform.position;
		if (this.useFlatDistance)
		{
			vector.y = 0f;
		}
		if (vector.sqrMagnitude >= this.sqrDistance || (this.automaticTimeout && this.automaticTimeoutTime < Time.time))
		{
			this.Timeout();
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0002EE7C File Offset: 0x0002D07C
	public void Timeout()
	{
		if (this.callback == null && this.callbackObject != null)
		{
			this.callback = this.callbackObject.GetComponent<IOnTimeout>();
		}
		if (this.callback != null)
		{
			this.callback.OnTimeout();
		}
		for (int i = 0; i < this.callbackObjects.Length; i++)
		{
			if (!(this.callbackObjects[i] == null))
			{
				this.callback = this.callbackObjects[i].GetComponent<IOnTimeout>();
				if (this.callback != null)
				{
					this.callback.OnTimeout();
				}
			}
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0000629A File Offset: 0x0000449A
	public void SetTimeoutTime(float timeFromNow)
	{
		this.automaticTimeoutTime = Time.time + timeFromNow;
	}

	// Token: 0x040007DF RID: 2015
	public float distance = 50f;

	// Token: 0x040007E0 RID: 2016
	public bool useFlatDistance = true;

	// Token: 0x040007E1 RID: 2017
	private float sqrDistance;

	// Token: 0x040007E2 RID: 2018
	public GameObject callbackObject;

	// Token: 0x040007E3 RID: 2019
	public IOnTimeout callback;

	// Token: 0x040007E4 RID: 2020
	public GameObject[] callbackObjects;

	// Token: 0x040007E5 RID: 2021
	public bool automaticTimeout;

	// Token: 0x040007E6 RID: 2022
	[ConditionalHide("automaticTimeout", true)]
	public float automaticTimeoutDelay = 10f;

	// Token: 0x040007E7 RID: 2023
	private float automaticTimeoutTime = -1f;
}
