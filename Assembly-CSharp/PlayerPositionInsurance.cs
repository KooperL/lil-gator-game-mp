using System;
using UnityEngine;

public class PlayerPositionInsurance : MonoBehaviour
{
	// Token: 0x06000D27 RID: 3367 RVA: 0x0004A400 File Offset: 0x00048600
	private void LateUpdate()
	{
		if (base.transform.position.IsNaN())
		{
			if (Player.movement.isRagdolling)
			{
				Player.movement.ClearMods();
			}
			Player.movement.SetPosition(this.lastValidPosition);
			DebugMessages.d.Display("Recovered position at " + Time.frameCount.ToString());
			return;
		}
		this.lastValidPosition = base.transform.position;
		if (PlayerOrbitCamera.active.transform.position.IsNaN())
		{
			PlayerOrbitCamera.active.ResetPosition();
		}
		if (MainCamera.t.position.IsNaN())
		{
			MainCamera.t.position = this.lastValidCameraPosition;
			return;
		}
		this.lastValidCameraPosition = MainCamera.t.position;
	}

	private Vector3 lastValidPosition;

	private Vector3 lastValidCameraPosition;
}
