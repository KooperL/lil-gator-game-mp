using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour
{
	// Token: 0x06001004 RID: 4100 RVA: 0x0000DD26 File Offset: 0x0000BF26
	public void OnEnable()
	{
		this.eventTime = Time.time + this.time;
		this.triggered = false;
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0000DD41 File Offset: 0x0000BF41
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
