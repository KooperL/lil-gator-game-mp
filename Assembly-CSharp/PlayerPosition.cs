using System;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x0000983C File Offset: 0x00007A3C
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
