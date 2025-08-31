using System;
using UnityEngine;

public class TrackDolly : MonoBehaviour
{
	// Token: 0x06000566 RID: 1382 RVA: 0x0001C90B File Offset: 0x0001AB0B
	private void OnValidate()
	{
		if (this.moveDolly == null)
		{
			this.moveDolly = base.GetComponent<MoveDolly>();
		}
	}

	public MoveDolly moveDolly;
}
