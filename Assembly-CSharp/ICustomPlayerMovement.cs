using System;
using UnityEngine;

// Token: 0x020001EE RID: 494
public interface ICustomPlayerMovement
{
	// Token: 0x06000A80 RID: 2688
	void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex);

	// Token: 0x06000A81 RID: 2689
	void Cancel();
}
