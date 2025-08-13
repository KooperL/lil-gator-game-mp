using System;
using UnityEngine;

// Token: 0x0200015D RID: 349
public class TrackDolly : MonoBehaviour
{
	// Token: 0x0600067E RID: 1662 RVA: 0x00006B02 File Offset: 0x00004D02
	private void OnValidate()
	{
		if (this.moveDolly == null)
		{
			this.moveDolly = base.GetComponent<MoveDolly>();
		}
	}

	// Token: 0x040008BB RID: 2235
	public MoveDolly moveDolly;
}
