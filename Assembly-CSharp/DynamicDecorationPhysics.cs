using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class DynamicDecorationPhysics : RigidbodyReset
{
	// Token: 0x060000F2 RID: 242 RVA: 0x00002C3C File Offset: 0x00000E3C
	public override void OnValidate()
	{
		if (this.dynamicDecoration == null)
		{
			this.dynamicDecoration = base.GetComponentInParent<DynamicDecoration>();
		}
		base.OnValidate();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00002C5E File Offset: 0x00000E5E
	public override void OnAwakeChange()
	{
		this.dynamicDecoration.IsAwake = this.isAwake || Vector3.SqrMagnitude(this.initialPosition - this.rigidbody.position) > 0.2f;
		base.OnAwakeChange();
	}

	// Token: 0x0400016B RID: 363
	public DynamicDecoration dynamicDecoration;
}
