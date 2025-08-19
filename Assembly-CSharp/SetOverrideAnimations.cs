using System;
using UnityEngine;

public class SetOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000D0A RID: 3338 RVA: 0x0000C17C File Offset: 0x0000A37C
	private void OnEnable()
	{
		if (this.playerOverrideAnimations == null)
		{
			this.playerOverrideAnimations = Player.overrideAnimations;
		}
		this.playerOverrideAnimations.SetOverrides(this.animationOverrides);
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
	private void OnDisable()
	{
		this.playerOverrideAnimations.ClearOverrides(this.animationOverrides);
	}

	private PlayerOverrideAnimations playerOverrideAnimations;

	public AnimationOverride[] animationOverrides;
}
