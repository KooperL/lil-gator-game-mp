using System;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public class UIRandomRotation : MonoBehaviour
{
	// Token: 0x0600126A RID: 4714 RVA: 0x0000F92D File Offset: 0x0000DB2D
	private void OnEnable()
	{
		base.transform.rotation = Quaternion.Euler(Random.Range(-this.angle, this.angle) * Vector3.forward);
	}

	// Token: 0x040017D5 RID: 6101
	public float angle = 25f;
}
