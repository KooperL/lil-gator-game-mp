using System;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
	// Token: 0x06000829 RID: 2089 RVA: 0x00027155 File Offset: 0x00025355
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
