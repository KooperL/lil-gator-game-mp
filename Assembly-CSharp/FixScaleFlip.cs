using System;
using UnityEngine;

public class FixScaleFlip : MonoBehaviour
{
	// Token: 0x06000ADB RID: 2779 RVA: 0x0003F6A0 File Offset: 0x0003D8A0
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
