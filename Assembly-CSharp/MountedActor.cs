using System;
using UnityEngine;
using UnityEngine.Events;

public class MountedActor : MonoBehaviour
{
	// Token: 0x060003EE RID: 1006 RVA: 0x000170E8 File Offset: 0x000152E8
	private void Awake()
	{
		PlayerActorStates component = base.GetComponent<PlayerActorStates>();
		if (component != null)
		{
			component.mountedActor = this;
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0001710C File Offset: 0x0001530C
	public void CancelMount()
	{
		this.mount.CancelMount();
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00017119 File Offset: 0x00015319
	public void GetOut()
	{
		if (this.mount != null)
		{
			this.mount.GetOut();
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00017134 File Offset: 0x00015334
	public void GottenOut()
	{
		this.onGottenOut.Invoke();
		this.onGottenOut = new UnityEvent();
	}

	public ActorMount mount;

	public UnityEvent onGottenOut = new UnityEvent();
}
