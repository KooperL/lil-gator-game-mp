using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
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

	// Token: 0x04000A54 RID: 2644
	public bool snapToFloor = true;
}
