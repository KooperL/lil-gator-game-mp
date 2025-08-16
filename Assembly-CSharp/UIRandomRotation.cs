using System;
using UnityEngine;

public class UIRandomRotation : MonoBehaviour
{
	// Token: 0x060012CA RID: 4810 RVA: 0x0000FD0E File Offset: 0x0000DF0E
	private void OnEnable()
	{
		base.transform.rotation = Quaternion.Euler(global::UnityEngine.Random.Range(-this.angle, this.angle) * Vector3.forward);
	}

	public float angle = 25f;
}
