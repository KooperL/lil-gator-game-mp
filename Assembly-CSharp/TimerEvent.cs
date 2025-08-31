using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvent : MonoBehaviour
{
	// Token: 0x06000CFB RID: 3323 RVA: 0x0003EAA9 File Offset: 0x0003CCA9
	public void OnEnable()
	{
		this.eventTime = Time.time + this.time;
		this.triggered = false;
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0003EAC4 File Offset: 0x0003CCC4
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
