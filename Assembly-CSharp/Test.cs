using System;
using Rewired;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class Test : MonoBehaviour
{
	// Token: 0x06000F82 RID: 3970 RVA: 0x0000D734 File Offset: 0x0000B934
	private void Start()
	{
		this.isToggled = Shader.IsKeywordEnabled("ENABLE_LIGHTING_BRANCHING");
		ReInput.players.GetPlayer(0).AddInputEventDelegate(new Action<InputActionEventData>(this.OnToggle), 0, 3, ReInput.mapping.GetActionId("DebugToggle"));
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0000D773 File Offset: 0x0000B973
	private void OnToggle(InputActionEventData obj)
	{
		this.isToggled = !this.isToggled;
		if (this.isToggled)
		{
			Shader.EnableKeyword("ENABLE_LIGHTING_BRANCHING");
		}
		else
		{
			Shader.DisableKeyword("ENABLE_LIGHTING_BRANCHING");
		}
		Debug.Log(this.isToggled);
	}

	// Token: 0x04001414 RID: 5140
	private bool isToggled;
}
