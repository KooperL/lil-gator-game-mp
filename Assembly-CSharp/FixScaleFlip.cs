using System;
using UnityEngine;

public class FixScaleFlip : MonoBehaviour
{
	// Token: 0x06000ADA RID: 2778 RVA: 0x0003F3B4 File Offset: 0x0003D5B4
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
