using System;
using UnityEngine;
using UnityEngine.Events;

public class PlatformEvents : MonoBehaviour
{
	// Token: 0x06000A8F RID: 2703 RVA: 0x0000A137 File Offset: 0x00008337
	private void Start()
	{
		if (this.pcEvent != null)
		{
			this.pcEvent.Invoke();
		}
	}

	public UnityEvent debugEvent;

	public UnityEvent nxEvent;

	public UnityEvent pcEvent;
}
