using System;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class FixScaleFlip : MonoBehaviour
{
	// Token: 0x06000909 RID: 2313 RVA: 0x0002B4D0 File Offset: 0x000296D0
	private void Start()
	{
		Vector3 lossyScale = base.transform.lossyScale;
		if (lossyScale.x * lossyScale.y * lossyScale.z < 0f)
		{
			Vector3 localScale = base.transform.localScale;
			localScale.x *= -1f;
			base.transform.localScale = localScale;
		}
	}
}
