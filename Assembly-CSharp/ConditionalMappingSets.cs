using System;
using Rewired.UI.ControlMapper;
using UnityEngine;

// Token: 0x020002A9 RID: 681
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

	// Token: 0x040012C0 RID: 4800
	public ControlMapper.MappingSet[] copiedMappingSets = new ControlMapper.MappingSet[] { ControlMapper.MappingSet.Default };

	// Token: 0x040012C1 RID: 4801
	public ControlMapper.MappingSet[] usualMappingSets;

	// Token: 0x040012C2 RID: 4802
	public ControlMapper.MappingSet[] merge1;

	// Token: 0x040012C3 RID: 4803
	public ControlMapper.MappingSet[] merge2;
}
