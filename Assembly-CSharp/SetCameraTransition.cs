using System;
using UnityEngine;

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
