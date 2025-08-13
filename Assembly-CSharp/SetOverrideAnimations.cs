using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class SetOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000CBE RID: 3262 RVA: 0x0000BE6A File Offset: 0x0000A06A
	private void OnEnable()
	{
		if (this.playerOverrideAnimations == null)
		{
			this.playerOverrideAnimations = Player.overrideAnimations;
		}
		this.playerOverrideAnimations.SetOverrides(this.animationOverrides);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0000BE96 File Offset: 0x0000A096
	private void OnDisable()
	{
		this.playerOverrideAnimations.ClearOverrides(this.animationOverrides);
	}

	// Token: 0x04001109 RID: 4361
	private PlayerOverrideAnimations playerOverrideAnimations;

	// Token: 0x0400110A RID: 4362
	public AnimationOverride[] animationOverrides;
}
