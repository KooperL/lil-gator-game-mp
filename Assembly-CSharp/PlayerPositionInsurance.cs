using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class PlayerPositionInsurance : MonoBehaviour
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x000485B0 File Offset: 0x000467B0
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

	// Token: 0x04001127 RID: 4391
	private Vector3 lastValidPosition;

	// Token: 0x04001128 RID: 4392
	private Vector3 lastValidCameraPosition;
}
