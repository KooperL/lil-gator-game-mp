using System;
using UnityEngine;

public class ChangeMovementSpeed : MonoBehaviour
{
	// Token: 0x06000B1A RID: 2842 RVA: 0x0000A79E File Offset: 0x0000899E
	public void Start()
	{
		Player.movement.overriddenSpeed = this.speed;
		Player.movement.overrideSpeed = true;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0000A7BB File Offset: 0x000089BB
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
