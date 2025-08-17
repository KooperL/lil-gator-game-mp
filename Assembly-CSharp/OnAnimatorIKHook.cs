using System;
using UnityEngine;

public class OnAnimatorIKHook : MonoBehaviour
{
	// Token: 0x06000A50 RID: 2640 RVA: 0x00009DEC File Offset: 0x00007FEC
	public void OnAnimatorIK()
	{
		this.actor.OnAnimatorIK();
	}

	public DialogueActor actor;
}
