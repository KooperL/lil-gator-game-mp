using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
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

	// Token: 0x04000767 RID: 1895
	public MoveDolly moveDolly;
}
