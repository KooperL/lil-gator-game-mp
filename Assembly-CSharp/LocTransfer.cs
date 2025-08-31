using System;
using UnityEngine;

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

	private static readonly string[] lineSeparators = new string[] { "\r\n", "\n" };

	private const char fieldSeparator = ',';

	public MultilingualTextDocument[] debugDocuments;

	public LocTransfer.DocumentSource[] documentSources;

	[Serializable]
	public struct DocumentSource
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x000666E4 File Offset: 0x000648E4
		public DocumentSource(MultilingualTextDocument document)
		{
			this.document = document;
			this.sourceFileName = document.name;
		}

		public string sourceFileName;

		public MultilingualTextDocument document;
	}
}
