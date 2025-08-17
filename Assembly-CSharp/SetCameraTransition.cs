using System;
using UnityEngine;

public class SetCameraTransition : MonoBehaviour
{
	// Token: 0x06000F3C RID: 3900 RVA: 0x000502A8 File Offset: 0x0004E4A8
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

	// Token: 0x06000F3D RID: 3901 RVA: 0x0000D323 File Offset: 0x0000B523
	private void Awake()
	{
		if (string.IsNullOrEmpty(this.transition))
		{
			this.transition = this.GetTransitionName(this.selectedTransition);
		}
		base.gameObject.name = this.transition;
	}

	[ReadOnly]
	public string transition;

	public SetCameraTransition.Transition selectedTransition;

	public enum Transition
	{
		Cut,
		Ease,
		EaseSlow,
		EaseVerySlow,
		EaseMedium
	}
}
