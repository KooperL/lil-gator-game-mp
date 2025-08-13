using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class DistanceTimeout : MonoBehaviour, IManagedUpdate
{
	// Token: 0x17000041 RID: 65
	// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001A10F File Offset: 0x0001830F
	public bool AutomaticTimeout
	{
		set
		{
			this.automaticTimeout = value;
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0001A118 File Offset: 0x00018318
	private void Awake()
	{
		this.sqrDistance = this.distance * this.distance;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0001A12D File Offset: 0x0001832D
	private void OnEnable()
	{
		if (this.automaticTimeout)
		{
			this.automaticTimeoutTime = Time.time + this.automaticTimeoutDelay;
		}
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0001A154 File Offset: 0x00018354
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0001A164 File Offset: 0x00018364
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

	// Token: 0x060004CC RID: 1228 RVA: 0x0001A1CC File Offset: 0x000183CC
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

	// Token: 0x060004CD RID: 1229 RVA: 0x0001A267 File Offset: 0x00018467
	public void SetTimeoutTime(float timeFromNow)
	{
		this.automaticTimeoutTime = Time.time + timeFromNow;
	}

	// Token: 0x0400069D RID: 1693
	public float distance = 50f;

	// Token: 0x0400069E RID: 1694
	public bool useFlatDistance = true;

	// Token: 0x0400069F RID: 1695
	private float sqrDistance;

	// Token: 0x040006A0 RID: 1696
	public GameObject callbackObject;

	// Token: 0x040006A1 RID: 1697
	public IOnTimeout callback;

	// Token: 0x040006A2 RID: 1698
	public GameObject[] callbackObjects;

	// Token: 0x040006A3 RID: 1699
	public bool automaticTimeout;

	// Token: 0x040006A4 RID: 1700
	[ConditionalHide("automaticTimeout", true)]
	public float automaticTimeoutDelay = 10f;

	// Token: 0x040006A5 RID: 1701
	private float automaticTimeoutTime = -1f;
}
