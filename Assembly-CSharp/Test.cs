using System;
using Rewired;
using UnityEngine;

public class Test : MonoBehaviour
{
	// Token: 0x06000FDF RID: 4063 RVA: 0x0000DAE6 File Offset: 0x0000BCE6
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
