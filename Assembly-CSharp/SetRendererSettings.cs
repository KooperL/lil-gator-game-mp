using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000FF RID: 255
public class SetRendererSettings : MonoBehaviour
{
	// Token: 0x06000541 RID: 1345 RVA: 0x0001C0AD File Offset: 0x0001A2AD
	private void OnEnable()
	{
		this.SetSettings();
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0001C0B5 File Offset: 0x0001A2B5
	[ContextMenu("Set Settings")]
	public void SetSettings()
	{
		base.GetComponent<Renderer>().shadowCastingMode = this.shadowCastingMode;
	}

	// Token: 0x04000736 RID: 1846
	public ShadowCastingMode shadowCastingMode = ShadowCastingMode.On;
}
