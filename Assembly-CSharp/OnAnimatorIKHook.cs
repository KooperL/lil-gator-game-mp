using System;
using UnityEngine;

public class OnAnimatorIKHook : MonoBehaviour
{
	// Token: 0x06000A51 RID: 2641 RVA: 0x00009DF6 File Offset: 0x00007FF6
	public void OnAnimatorIK()
	{
		this.actor.OnAnimatorIK();
	}

	public DialogueActor actor;
}
