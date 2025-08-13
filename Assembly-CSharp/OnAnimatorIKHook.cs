using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class OnAnimatorIKHook : MonoBehaviour
{
	// Token: 0x06000A08 RID: 2568 RVA: 0x00009AB8 File Offset: 0x00007CB8
	public void OnAnimatorIK()
	{
		this.actor.OnAnimatorIK();
	}

	// Token: 0x04000C82 RID: 3202
	public DialogueActor actor;
}
