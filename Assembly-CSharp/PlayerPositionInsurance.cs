using System;
using UnityEngine;

// Token: 0x020001FD RID: 509
public class PlayerPositionInsurance : MonoBehaviour
{
	// Token: 0x06000B15 RID: 2837 RVA: 0x000375B4 File Offset: 0x000357B4
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

	// Token: 0x04000EC8 RID: 3784
	private Vector3 lastValidPosition;

	// Token: 0x04000EC9 RID: 3785
	private Vector3 lastValidCameraPosition;
}
