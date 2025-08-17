using System;
using UnityEngine;

public class DistanceTimeout : MonoBehaviour, IManagedUpdate
{
	// (set) Token: 0x0600060D RID: 1549 RVA: 0x0000651B File Offset: 0x0000471B
	public bool AutomaticTimeout
	{
		set
		{
			this.automaticTimeout = value;
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x00006524 File Offset: 0x00004724
	private void Awake()
	{
		this.sqrDistance = this.distance * this.distance;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00006539 File Offset: 0x00004739
	private void OnEnable()
	{
		if (this.automaticTimeout)
		{
			this.automaticTimeoutTime = Time.time + this.automaticTimeoutDelay;
		}
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x000026CE File Offset: 0x000008CE
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00030510 File Offset: 0x0002E710
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

	// Token: 0x06000612 RID: 1554 RVA: 0x00030578 File Offset: 0x0002E778
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
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00006560 File Offset: 0x00004760
	public void SetTimeoutTime(float timeFromNow)
	{
		this.automaticTimeoutTime = Time.time + timeFromNow;
	}

	public float distance = 50f;

	public bool useFlatDistance = true;

	private float sqrDistance;

	public GameObject callbackObject;

	public IOnTimeout callback;

	public GameObject[] callbackObjects;

	public bool automaticTimeout;

	[ConditionalHide("automaticTimeout", true)]
	public float automaticTimeoutDelay = 10f;

	private float automaticTimeoutTime = -1f;
}
