using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class ChunkLookupAttribute : PropertyAttribute
{
	// Token: 0x06000CF1 RID: 3313 RVA: 0x0000C039 File Offset: 0x0000A239
	public ChunkLookupAttribute(string documentSourceField)
	{
		this.documentSourceField = documentSourceField;
	}

	// Token: 0x04001152 RID: 4434
	public string documentSourceField;
}
