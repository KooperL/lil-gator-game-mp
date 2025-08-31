using System;
using UnityEngine;

public class MaintainAnimationDirection : StateMachineBehaviour
{
	// Token: 0x060007A3 RID: 1955 RVA: 0x000256BD File Offset: 0x000238BD
	private void Initialize(Animator animator, AnimatorStateInfo stateInfo)
	{
		this.animator = animator;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x000256C6 File Offset: 0x000238C6
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x000256C8 File Offset: 0x000238C8
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x000256CA File Offset: 0x000238CA
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	private Animator animator;

	private AnimationClip clip;

	private bool isInitialized;
}
