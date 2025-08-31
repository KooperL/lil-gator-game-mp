using System;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{
	// Token: 0x060007CA RID: 1994 RVA: 0x0002601F File Offset: 0x0002421F
	public void OnValidate()
	{
		if (this.matchPlayer)
		{
			this.transformTarget = GameObject.FindWithTag("Player").transform;
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002603E File Offset: 0x0002423E
	private void Start()
	{
		this.Match();
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00026046 File Offset: 0x00024246
	private void LateUpdate()
	{
		this.Match();
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00026050 File Offset: 0x00024250
	[ContextMenu("Match")]
	private void Match()
	{
		if (this.transformTarget == null)
		{
			return;
		}
		if (this.matchRotation)
		{
			base.transform.rotation = this.transformTarget.rotation;
		}
		if (this.matchPosition)
		{
			base.transform.position = this.transformTarget.position;
		}
	}

	public bool matchPlayer = true;

	[ConditionalHide("matchPlayer", true, Inverse = true)]
	public Transform transformTarget;

	public bool matchPosition = true;

	public bool matchRotation = true;
}
