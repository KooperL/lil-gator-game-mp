using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class ActorVisibility : MonoBehaviour
{
	// Token: 0x06000335 RID: 821 RVA: 0x00012FA3 File Offset: 0x000111A3
	private void OnValidate()
	{
		this.UpdateRenderers();
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00012FAB File Offset: 0x000111AB
	[ContextMenu("Update Renderers")]
	public void UpdateRenderers()
	{
		this.renderers = base.GetComponentsInChildren<Renderer>();
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00012FBC File Offset: 0x000111BC
	public void Show()
	{
		foreach (Renderer renderer in this.renderers)
		{
			if (renderer != null)
			{
				renderer.enabled = true;
			}
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00012FF4 File Offset: 0x000111F4
	public void Hide()
	{
		foreach (Renderer renderer in this.renderers)
		{
			if (renderer != null)
			{
				renderer.enabled = false;
			}
		}
	}

	// Token: 0x04000468 RID: 1128
	public Renderer[] renderers;
}
