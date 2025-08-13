using System;
using Rewired.UI.ControlMapper;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class ConditionalMappingSets : MonoBehaviour
{
	// Token: 0x06001138 RID: 4408 RVA: 0x00056968 File Offset: 0x00054B68
	[ContextMenu("Copy mapping sets")]
	public void CopyMappingSets()
	{
		ControlMapper component = base.GetComponent<ControlMapper>();
		this.copiedMappingSets = component._mappingSets;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0000EC13 File Offset: 0x0000CE13
	[ContextMenu("Set mapping set")]
	public void SetMappingSet()
	{
		ControlMapper component = base.GetComponent<ControlMapper>();
		component._mappingSets = this.merge2;
		component.Reset();
	}

	// Token: 0x04001628 RID: 5672
	public ControlMapper.MappingSet[] copiedMappingSets = new ControlMapper.MappingSet[] { ControlMapper.MappingSet.Default };

	// Token: 0x04001629 RID: 5673
	public ControlMapper.MappingSet[] usualMappingSets;

	// Token: 0x0400162A RID: 5674
	public ControlMapper.MappingSet[] merge1;

	// Token: 0x0400162B RID: 5675
	public ControlMapper.MappingSet[] merge2;
}
