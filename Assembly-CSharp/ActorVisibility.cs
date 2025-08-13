using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class ActorVisibility : MonoBehaviour
{
	// Token: 0x0600037A RID: 890 RVA: 0x00004AA1 File Offset: 0x00002CA1
	private void OnValidate()
	{
		this.UpdateRenderers();
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00004AA9 File Offset: 0x00002CA9
	[ContextMenu("Update Renderers")]
	public void UpdateRenderers()
	{
		this.renderers = base.GetComponentsInChildren<Renderer>();
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000262E8 File Offset: 0x000244E8
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

	// Token: 0x0600037D RID: 893 RVA: 0x00026320 File Offset: 0x00024520
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

	// Token: 0x04000506 RID: 1286
	public Renderer[] renderers;
}
