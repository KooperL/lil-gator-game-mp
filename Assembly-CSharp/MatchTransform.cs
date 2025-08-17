using System;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{
	// Token: 0x0600096D RID: 2413 RVA: 0x0000926E File Offset: 0x0000746E
	public void OnValidate()
	{
		if (this.matchPlayer)
		{
			this.transformTarget = GameObject.FindWithTag("Player").transform;
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0000928D File Offset: 0x0000748D
	private void Start()
	{
		this.Match();
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0000928D File Offset: 0x0000748D
	private void LateUpdate()
	{
		this.Match();
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0003AAF8 File Offset: 0x00038CF8
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
