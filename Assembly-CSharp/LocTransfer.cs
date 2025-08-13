using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class LocTransfer : ScriptableObject
{
	// Token: 0x0600003F RID: 63 RVA: 0x00002DCC File Offset: 0x00000FCC
	public string GetLocDocumentName(MultilingualTextDocument document)
	{
		foreach (LocTransfer.DocumentSource documentSource in this.documentSources)
		{
			if (documentSource.document == document)
			{
				return documentSource.sourceFileName;
			}
		}
		return document.name;
	}

	// Token: 0x04000058 RID: 88
	private static readonly string[] lineSeparators = new string[] { "\r\n", "\n" };

	// Token: 0x04000059 RID: 89
	private const char fieldSeparator = ',';

	// Token: 0x0400005A RID: 90
	public MultilingualTextDocument[] debugDocuments;

	// Token: 0x0400005B RID: 91
	public LocTransfer.DocumentSource[] documentSources;

	// Token: 0x0200034F RID: 847
	[Serializable]
	public struct DocumentSource
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x000666E4 File Offset: 0x000648E4
		public DocumentSource(MultilingualTextDocument document)
		{
			this.document = document;
			this.sourceFileName = document.name;
		}

		// Token: 0x040019C2 RID: 6594
		public string sourceFileName;

		// Token: 0x040019C3 RID: 6595
		public MultilingualTextDocument document;
	}
}
