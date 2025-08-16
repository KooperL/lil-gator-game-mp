using System;
using UnityEngine;
using UnityEngine.Rendering;

public class SetRendererSettings : MonoBehaviour
{
	// Token: 0x0600068D RID: 1677 RVA: 0x00006B91 File Offset: 0x00004D91
	private void OnEnable()
	{
		this.SetSettings();
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00006B99 File Offset: 0x00004D99
	[ContextMenu("Set Settings")]
	public void SetSettings()
	{
		base.GetComponent<Renderer>().shadowCastingMode = this.shadowCastingMode;
	}

	public ShadowCastingMode shadowCastingMode = ShadowCastingMode.On;
}
