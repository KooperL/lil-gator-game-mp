using System;
using Rewired;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class Test : MonoBehaviour
{
	// Token: 0x06000CD6 RID: 3286 RVA: 0x0003E240 File Offset: 0x0003C440
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

	// Token: 0x040010F7 RID: 4343
	public Texture2D[] textures;

	// Token: 0x040010F8 RID: 4344
	private bool isToggled;
}
