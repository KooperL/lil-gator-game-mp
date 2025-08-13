using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class SetOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000AFF RID: 2815 RVA: 0x00037128 File Offset: 0x00035328
	private void OnEnable()
	{
		if (this.playerOverrideAnimations == null)
		{
			this.playerOverrideAnimations = Player.overrideAnimations;
		}
		this.playerOverrideAnimations.SetOverrides(this.animationOverrides);
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00037154 File Offset: 0x00035354
	private void OnDisable()
	{
		this.playerOverrideAnimations.ClearOverrides(this.animationOverrides);
	}

	// Token: 0x04000EAE RID: 3758
	private PlayerOverrideAnimations playerOverrideAnimations;

	// Token: 0x04000EAF RID: 3759
	public AnimationOverride[] animationOverrides;
}
