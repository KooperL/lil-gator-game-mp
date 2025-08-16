using System;
using Rewired;
using UnityEngine;

public class Test : MonoBehaviour
{
	// Token: 0x06000FDE RID: 4062 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
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

	public Texture2D[] textures;

	private bool isToggled;
}
