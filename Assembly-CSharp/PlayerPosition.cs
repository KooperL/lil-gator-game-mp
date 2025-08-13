using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
public class PlayerPosition : MonoBehaviour
{
	// Token: 0x06000998 RID: 2456 RVA: 0x000094FE File Offset: 0x000076FE
	private void Start()
	{
		if (this.snapToFloor)
		{
			LayerUtil.SnapToGround(base.transform, 5f);
		}
		Player.movement.ApplyTransform(base.transform);
	}

	// Token: 0x04000C40 RID: 3136
	public bool snapToFloor = true;
}
