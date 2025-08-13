using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class OnAnimatorIKHook : MonoBehaviour
{
	// Token: 0x06000885 RID: 2181 RVA: 0x0002841F File Offset: 0x0002661F
	public void OnAnimatorIK()
	{
		this.actor.OnAnimatorIK();
	}

	// Token: 0x04000A7D RID: 2685
	public DialogueActor actor;
}
