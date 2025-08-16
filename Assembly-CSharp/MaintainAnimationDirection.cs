using System;
using UnityEngine;

public class MaintainAnimationDirection : StateMachineBehaviour
{
	// Token: 0x06000942 RID: 2370 RVA: 0x00009009 File Offset: 0x00007209
	private void Initialize(Animator animator, AnimatorStateInfo stateInfo)
	{
		this.animator = animator;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00002229 File Offset: 0x00000429
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00002229 File Offset: 0x00000429
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00002229 File Offset: 0x00000429
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	private Animator animator;

	private AnimationClip clip;

	private bool isInitialized;
}
