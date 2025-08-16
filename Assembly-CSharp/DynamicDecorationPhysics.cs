using System;
using UnityEngine;

public class DynamicDecorationPhysics : RigidbodyReset
{
	// Token: 0x060000FA RID: 250 RVA: 0x00002CA0 File Offset: 0x00000EA0
	public override void OnValidate()
	{
		if (this.dynamicDecoration == null)
		{
			this.dynamicDecoration = base.GetComponentInParent<DynamicDecoration>();
		}
		base.OnValidate();
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00002CC2 File Offset: 0x00000EC2
	public override void OnAwakeChange()
	{
		this.dynamicDecoration.IsAwake = this.isAwake || Vector3.SqrMagnitude(this.initialPosition - this.rigidbody.position) > 0.2f;
		base.OnAwakeChange();
	}

	public DynamicDecoration dynamicDecoration;
}
