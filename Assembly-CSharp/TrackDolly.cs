using System;
using UnityEngine;

public class TrackDolly : MonoBehaviour
{
	// Token: 0x060006B8 RID: 1720 RVA: 0x00006DC8 File Offset: 0x00004FC8
	private void OnValidate()
	{
		if (this.moveDolly == null)
		{
			this.moveDolly = base.GetComponent<MoveDolly>();
		}
	}

	public MoveDolly moveDolly;
}
