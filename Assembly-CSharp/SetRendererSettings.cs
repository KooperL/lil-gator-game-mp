using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000152 RID: 338
public class SetRendererSettings : MonoBehaviour
{
	// Token: 0x06000653 RID: 1619 RVA: 0x000068CB File Offset: 0x00004ACB
	private void OnEnable()
	{
		this.SetSettings();
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x000068D3 File Offset: 0x00004AD3
	[ContextMenu("Set Settings")]
	public void SetSettings()
	{
		base.GetComponent<Renderer>().shadowCastingMode = this.shadowCastingMode;
	}

	// Token: 0x0400087D RID: 2173
	public ShadowCastingMode shadowCastingMode = 1;
}
