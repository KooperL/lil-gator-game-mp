using System;
using UnityEngine;

public class OnAnimatorIKHook : MonoBehaviour
{
	// Token: 0x06000885 RID: 2181 RVA: 0x0002841F File Offset: 0x0002661F
	public void OnAnimatorIK()
	{
		this.actor.OnAnimatorIK();
	}

	public DialogueActor actor;
}
