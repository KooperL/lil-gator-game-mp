using System;

public class TextLookupAttribute : LookupAttribute
{
	// Token: 0x06000D48 RID: 3400 RVA: 0x00002ABF File Offset: 0x00000CBF
	public TextLookupAttribute(string documentSourceField)
	{
		this.sourceField = documentSourceField;
	}
}
