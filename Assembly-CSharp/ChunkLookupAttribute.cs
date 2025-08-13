using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class ChunkLookupAttribute : PropertyAttribute
{
	// Token: 0x06000B26 RID: 2854 RVA: 0x00037B61 File Offset: 0x00035D61
	public ChunkLookupAttribute(string documentSourceField)
	{
		this.documentSourceField = documentSourceField;
	}

	// Token: 0x04000EEC RID: 3820
	public string documentSourceField;
}
