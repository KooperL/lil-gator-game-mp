using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class SetCameraTransition : MonoBehaviour
{
	// Token: 0x06000EE0 RID: 3808 RVA: 0x0004E458 File Offset: 0x0004C658
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

	// Token: 0x06000EE1 RID: 3809 RVA: 0x0000CF7B File Offset: 0x0000B17B
	private void Awake()
	{
		if (string.IsNullOrEmpty(this.transition))
		{
			this.transition = this.GetTransitionName(this.selectedTransition);
		}
		base.gameObject.name = this.transition;
	}

	// Token: 0x040012F8 RID: 4856
	[ReadOnly]
	public string transition;

	// Token: 0x040012F9 RID: 4857
	public SetCameraTransition.Transition selectedTransition;

	// Token: 0x020002F3 RID: 755
	public enum Transition
	{
		// Token: 0x040012FB RID: 4859
		Cut,
		// Token: 0x040012FC RID: 4860
		Ease,
		// Token: 0x040012FD RID: 4861
		EaseSlow,
		// Token: 0x040012FE RID: 4862
		EaseVerySlow,
		// Token: 0x040012FF RID: 4863
		EaseMedium
	}
}
