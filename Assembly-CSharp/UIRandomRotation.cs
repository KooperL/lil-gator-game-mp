using System;
using UnityEngine;

public class UIRandomRotation : MonoBehaviour
{
	// Token: 0x060012CB RID: 4811 RVA: 0x0000FD2D File Offset: 0x0000DF2D
	private void OnEnable()
	{
		base.transform.rotation = Quaternion.Euler(global::UnityEngine.Random.Range(-this.angle, this.angle) * Vector3.forward);
	}

	public float angle = 25f;
}
