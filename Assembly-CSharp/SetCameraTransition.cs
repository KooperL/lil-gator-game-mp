using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class SetCameraTransition : MonoBehaviour
{
	// Token: 0x06000C41 RID: 3137 RVA: 0x0003AF98 File Offset: 0x00039198
	private string GetTransitionName(SetCameraTransition.Transition transition)
	{
		switch (transition)
		{
		case SetCameraTransition.Transition.Cut:
			return "CM Cut";
		case SetCameraTransition.Transition.Ease:
			return "CM Ease";
		case SetCameraTransition.Transition.EaseSlow:
			return "CM Ease Slow";
		case SetCameraTransition.Transition.EaseVerySlow:
			return "CM Ease Very Slow";
		case SetCameraTransition.Transition.EaseMedium:
			return "CM Ease Medium";
		default:
			return "";
		}
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x0003AFE4 File Offset: 0x000391E4
	private void Awake()
	{
		if (string.IsNullOrEmpty(this.transition))
		{
			this.transition = this.GetTransitionName(this.selectedTransition);
		}
		base.gameObject.name = this.transition;
	}

	// Token: 0x04000FFD RID: 4093
	[ReadOnly]
	public string transition;

	// Token: 0x04000FFE RID: 4094
	public SetCameraTransition.Transition selectedTransition;

	// Token: 0x0200041C RID: 1052
	public enum Transition
	{
		// Token: 0x04001D2E RID: 7470
		Cut,
		// Token: 0x04001D2F RID: 7471
		Ease,
		// Token: 0x04001D30 RID: 7472
		EaseSlow,
		// Token: 0x04001D31 RID: 7473
		EaseVerySlow,
		// Token: 0x04001D32 RID: 7474
		EaseMedium
	}
}
