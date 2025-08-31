using System;
using UnityEngine;
using UnityEngine.Events;

public class PlatformEvents : MonoBehaviour
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x0002969C File Offset: 0x0002789C
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
