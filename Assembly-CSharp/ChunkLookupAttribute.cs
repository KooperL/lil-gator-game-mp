using System;
using UnityEngine;

public class ChunkLookupAttribute : PropertyAttribute
{
	// Token: 0x06000D3E RID: 3390 RVA: 0x0000C34B File Offset: 0x0000A54B
	public ChunkLookupAttribute(string documentSourceField)
	{
		this.documentSourceField = documentSourceField;
	}

	public string documentSourceField;
}
