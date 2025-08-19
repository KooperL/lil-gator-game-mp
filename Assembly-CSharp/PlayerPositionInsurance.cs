using System;
using UnityEngine;

public class PlayerPositionInsurance : MonoBehaviour
{
	// Token: 0x06000D26 RID: 3366 RVA: 0x0004A114 File Offset: 0x00048314
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
