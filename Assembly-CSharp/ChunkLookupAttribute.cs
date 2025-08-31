using System;
using UnityEngine;

public class ChunkLookupAttribute : PropertyAttribute
{
	// Token: 0x06000B26 RID: 2854 RVA: 0x00037B61 File Offset: 0x00035D61
	public ChunkLookupAttribute(string documentSourceField)
	{
		this.documentSourceField = documentSourceField;
	}

	public string documentSourceField;
}
