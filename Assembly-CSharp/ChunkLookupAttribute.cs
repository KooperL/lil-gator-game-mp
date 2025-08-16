using System;
using UnityEngine;

public class ChunkLookupAttribute : PropertyAttribute
{
	// Token: 0x06000D3D RID: 3389 RVA: 0x0000C32C File Offset: 0x0000A52C
	public ChunkLookupAttribute(string documentSourceField)
	{
		this.documentSourceField = documentSourceField;
	}

	public string documentSourceField;
}
