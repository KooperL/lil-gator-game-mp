using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200025B RID: 603
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

	// Token: 0x04001110 RID: 4368
	public UnityEvent unityEvent;

	// Token: 0x04001111 RID: 4369
	public float time = 5f;

	// Token: 0x04001112 RID: 4370
	private float eventTime = -1f;

	// Token: 0x04001113 RID: 4371
	private bool triggered;
}
