using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public interface ICustomPlayerMovement
{
	// Token: 0x06000C35 RID: 3125
	void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex);

	// Token: 0x06000C36 RID: 3126
	void Cancel();
}
