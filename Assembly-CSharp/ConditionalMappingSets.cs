using System;
using Rewired.UI.ControlMapper;
using UnityEngine;

public class ConditionalMappingSets : MonoBehaviour
{
	// Token: 0x06001198 RID: 4504 RVA: 0x00058928 File Offset: 0x00056B28
	[ContextMenu("Copy mapping sets")]
	public void CopyMappingSets()
	{
		ControlMapper component = base.GetComponent<ControlMapper>();
		this.copiedMappingSets = component._mappingSets;
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0000EFFC File Offset: 0x0000D1FC
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
