using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000321 RID: 801
public class TimerEvent : MonoBehaviour
{
	// Token: 0x06000FA8 RID: 4008 RVA: 0x0000D9B3 File Offset: 0x0000BBB3
	public void OnEnable()
	{
		this.eventTime = Time.time + this.time;
		this.triggered = false;
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0000D9CE File Offset: 0x0000BBCE
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

	// Token: 0x0400142C RID: 5164
	public UnityEvent unityEvent;

	// Token: 0x0400142D RID: 5165
	public float time = 5f;

	// Token: 0x0400142E RID: 5166
	private float eventTime = -1f;

	// Token: 0x0400142F RID: 5167
	private bool triggered;
}
