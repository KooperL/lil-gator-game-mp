using System;
using UnityEngine;

public class ActorVisibility : MonoBehaviour
{
	// Token: 0x060003A0 RID: 928 RVA: 0x00004C85 File Offset: 0x00002E85
	private void OnValidate()
	{
		this.UpdateRenderers();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00004C8D File Offset: 0x00002E8D
	[ContextMenu("Update Renderers")]
	public void UpdateRenderers()
	{
		this.renderers = base.GetComponentsInChildren<Renderer>();
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x000272A8 File Offset: 0x000254A8
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

	// Token: 0x060003A3 RID: 931 RVA: 0x000272E0 File Offset: 0x000254E0
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

	public Renderer[] renderers;
}
