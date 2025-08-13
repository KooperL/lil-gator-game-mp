using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class MatchTransform : MonoBehaviour
{
	// Token: 0x0600092C RID: 2348 RVA: 0x00008F3D File Offset: 0x0000713D
	public void OnValidate()
	{
		if (this.matchPlayer)
		{
			this.transformTarget = GameObject.FindWithTag("Player").transform;
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00008F5C File Offset: 0x0000715C
	private void Start()
	{
		this.Match();
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00008F5C File Offset: 0x0000715C
	private void LateUpdate()
	{
		this.Match();
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00039188 File Offset: 0x00037388
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

	// Token: 0x04000BD9 RID: 3033
	public bool matchPlayer = true;

	// Token: 0x04000BDA RID: 3034
	[ConditionalHide("matchPlayer", true, Inverse = true)]
	public Transform transformTarget;

	// Token: 0x04000BDB RID: 3035
	public bool matchPosition = true;

	// Token: 0x04000BDC RID: 3036
	public bool matchRotation = true;
}
