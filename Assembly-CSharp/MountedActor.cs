using System;
using UnityEngine;
using UnityEngine.Events;

public class MountedActor : MonoBehaviour
{
	// Token: 0x060004C8 RID: 1224 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
	private void Awake()
	{
		PlayerActorStates component = base.GetComponent<PlayerActorStates>();
		if (component != null)
		{
			component.mountedActor = this;
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x000057AA File Offset: 0x000039AA
	public void CancelMount()
	{
		this.mount.CancelMount();
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000057B7 File Offset: 0x000039B7
	public void GetOut()
	{
		if (this.mount != null)
		{
			this.mount.GetOut();
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000057D2 File Offset: 0x000039D2
	public void GottenOut()
	{
		this.onGottenOut.Invoke();
		this.onGottenOut = new UnityEvent();
	}

	public ActorMount mount;

	public UnityEvent onGottenOut = new UnityEvent();
}
