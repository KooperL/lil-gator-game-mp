using System;
using UnityEngine;

public class OnAnimatorIKHook : MonoBehaviour
{
	// Token: 0x06000A50 RID: 2640 RVA: 0x00009DF6 File Offset: 0x00007FF6
	public void OnAnimatorIK()
	{
		this.actor.OnAnimatorIK();
	}

	public DialogueActor actor;
}
