using System;
using UnityEngine;

public class LocTransfer : ScriptableObject
{
	// Token: 0x0600003F RID: 63 RVA: 0x00018134 File Offset: 0x00016334
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

	private static readonly string[] lineSeparators = new string[] { "\r\n", "\n" };

	private const char fieldSeparator = ',';

	public MultilingualTextDocument[] debugDocuments;

	public LocTransfer.DocumentSource[] documentSources;

	[Serializable]
	public struct DocumentSource
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002333 File Offset: 0x00000533
		public DocumentSource(MultilingualTextDocument document)
		{
			this.document = document;
			this.sourceFileName = document.name;
		}

		public string sourceFileName;

		public MultilingualTextDocument document;
	}
}
