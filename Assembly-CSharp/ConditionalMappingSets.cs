using System;
using Rewired.UI.ControlMapper;
using UnityEngine;

public class ConditionalMappingSets : MonoBehaviour
{
	// Token: 0x06000E68 RID: 3688 RVA: 0x00045008 File Offset: 0x00043208
	[ContextMenu("Copy mapping sets")]
	public void CopyMappingSets()
	{
		ControlMapper component = base.GetComponent<ControlMapper>();
		this.copiedMappingSets = component._mappingSets;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x00045028 File Offset: 0x00043228
	[ContextMenu("Set mapping set")]
	public void SetMappingSet()
	{
		ControlMapper component = base.GetComponent<ControlMapper>();
		component._mappingSets = this.merge2;
		component.Reset();
	}

	public ControlMapper.MappingSet[] copiedMappingSets = new ControlMapper.MappingSet[] { ControlMapper.MappingSet.Default };

	public ControlMapper.MappingSet[] usualMappingSets;

	public ControlMapper.MappingSet[] merge1;

	public ControlMapper.MappingSet[] merge2;
}
