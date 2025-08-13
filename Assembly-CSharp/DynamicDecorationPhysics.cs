using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class DynamicDecorationPhysics : RigidbodyReset
{
	// Token: 0x060000D9 RID: 217 RVA: 0x00006272 File Offset: 0x00004472
	public override void OnValidate()
	{
		if (this.dynamicDecoration == null)
		{
			this.dynamicDecoration = base.GetComponentInParent<DynamicDecoration>();
		}
		base.OnValidate();
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00006294 File Offset: 0x00004494
	public override void OnAwakeChange()
	{
		this.dynamicDecoration.IsAwake = this.isAwake || Vector3.SqrMagnitude(this.initialPosition - this.rigidbody.position) > 0.2f;
		base.OnAwakeChange();
	}

	// Token: 0x0400012B RID: 299
	public DynamicDecoration dynamicDecoration;
}
