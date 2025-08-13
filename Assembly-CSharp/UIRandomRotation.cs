using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class UIRandomRotation : MonoBehaviour
{
	// Token: 0x06000F56 RID: 3926 RVA: 0x00049C0C File Offset: 0x00047E0C
	private void OnEnable()
	{
		base.transform.rotation = Quaternion.Euler(Random.Range(-this.angle, this.angle) * Vector3.forward);
	}

	// Token: 0x04001430 RID: 5168
	public float angle = 25f;
}
