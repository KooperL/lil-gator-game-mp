using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class ChangeMovementSpeed : MonoBehaviour
{
	// Token: 0x06000ACD RID: 2765 RVA: 0x0000A460 File Offset: 0x00008660
	public void Start()
	{
		Player.movement.overriddenSpeed = this.speed;
		Player.movement.overrideSpeed = true;
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0000A47D File Offset: 0x0000867D
	private void OnDestroy()
	{
		if (Player.movement == null)
		{
			return;
		}
		Player.movement.overrideSpeed = false;
	}

	// Token: 0x04000DB2 RID: 3506
	public float speed = 5.5f;
}
