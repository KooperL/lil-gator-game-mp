using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000F5 RID: 245
public class MountedActor : MonoBehaviour
{
	// Token: 0x060004A1 RID: 1185 RVA: 0x0002B44C File Offset: 0x0002964C
	private void Awake()
	{
		PlayerActorStates component = base.GetComponent<PlayerActorStates>();
		if (component != null)
		{
			component.mountedActor = this;
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00005577 File Offset: 0x00003777
	public void CancelMount()
	{
		this.mount.CancelMount();
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00005584 File Offset: 0x00003784
	public void GetOut()
	{
		if (this.mount != null)
		{
			this.mount.GetOut();
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0000559F File Offset: 0x0000379F
	public void GottenOut()
	{
		this.onGottenOut.Invoke();
		this.onGottenOut = new UnityEvent();
	}

	// Token: 0x04000687 RID: 1671
	public ActorMount mount;

	// Token: 0x04000688 RID: 1672
	public UnityEvent onGottenOut = new UnityEvent();
}
