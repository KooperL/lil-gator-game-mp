using System;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
	// Token: 0x060009DF RID: 2527 RVA: 0x00009832 File Offset: 0x00007A32
	private void Start()
	{
		if (this.snapToFloor)
		{
			LayerUtil.SnapToGround(base.transform, 5f);
		}
		Player.movement.ApplyTransform(base.transform);
	}

	public bool snapToFloor = true;
}
