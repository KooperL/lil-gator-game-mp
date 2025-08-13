using System;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class MaintainAnimationDirection : StateMachineBehaviour
{
	// Token: 0x06000901 RID: 2305 RVA: 0x00008CED File Offset: 0x00006EED
	private void Initialize(Animator animator, AnimatorStateInfo stateInfo)
	{
		this.animator = animator;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00002229 File Offset: 0x00000429
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00002229 File Offset: 0x00000429
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00002229 File Offset: 0x00000429
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	// Token: 0x04000B9C RID: 2972
	private Animator animator;

	// Token: 0x04000B9D RID: 2973
	private AnimationClip clip;

	// Token: 0x04000B9E RID: 2974
	private bool isInitialized;
}
