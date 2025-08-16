using System;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
	// Token: 0x060009DF RID: 2527 RVA: 0x0000981D File Offset: 0x00007A1D
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
