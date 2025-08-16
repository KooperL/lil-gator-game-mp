using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour
{
	// Token: 0x06001003 RID: 4099 RVA: 0x0000DD07 File Offset: 0x0000BF07
	public void OnEnable()
	{
		this.eventTime = Time.time + this.time;
		this.triggered = false;
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0000DD22 File Offset: 0x0000BF22
	private void Update()
	{
		if (this.triggered)
		{
			return;
		}
		if (Time.time > this.eventTime)
		{
			this.unityEvent.Invoke();
			this.triggered = true;
		}
	}

	public UnityEvent unityEvent;

	public float time = 5f;

	private float eventTime = -1f;

	private bool triggered;
}
