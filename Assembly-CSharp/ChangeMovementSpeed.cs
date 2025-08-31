using System;
using UnityEngine;

public class ChangeMovementSpeed : MonoBehaviour
{
	// Token: 0x06000936 RID: 2358 RVA: 0x0002BDE5 File Offset: 0x00029FE5
	public void Start()
	{
		Player.movement.overriddenSpeed = this.speed;
		Player.movement.overrideSpeed = true;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0002BE02 File Offset: 0x0002A002
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
