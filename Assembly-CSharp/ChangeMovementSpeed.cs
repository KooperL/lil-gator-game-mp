using System;
using UnityEngine;

public class ChangeMovementSpeed : MonoBehaviour
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x0000A794 File Offset: 0x00008994
	public void Start()
	{
		Player.movement.overriddenSpeed = this.speed;
		Player.movement.overrideSpeed = true;
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0000A7B1 File Offset: 0x000089B1
	private void OnDestroy()
	{
		if (Player.movement == null)
		{
			return;
		}
		Player.movement.overrideSpeed = false;
	}

	public float speed = 5.5f;
}
