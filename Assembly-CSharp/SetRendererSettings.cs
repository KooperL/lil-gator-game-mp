using System;
using UnityEngine;
using UnityEngine.Rendering;

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

	public ShadowCastingMode shadowCastingMode = ShadowCastingMode.On;
}
