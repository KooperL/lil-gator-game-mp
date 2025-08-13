using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000E RID: 14
[RequireComponent(typeof(Text))]
public class MLText : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x000175FC File Offset: 0x000157FC
	public void OnValidate()
	{
		if (this.text == null)
		{
			this.text = base.GetComponent<Text>();
		}
		if (string.IsNullOrEmpty(this.idString) && this.document != null && this.text != null && !string.IsNullOrEmpty(this.text.text))
		{
			this.idString = this.document.FindString(this.text.text);
		}
		this.idInt = Animator.StringToHash(this.idString);
		if (this.document != null && this.document.HasString(this.idString))
		{
			this.text.text = this.document.FetchString(this.idInt, Language.English);
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000176CC File Offset: 0x000158CC
	[ContextMenu("Add entry to document")]
	public void AddEntryToDocument()
	{
		if (this.document != null && this.text != null && !this.document.HasString(this.idString))
		{
			MLTextUtil.AddMLStringEntry(this.document, this.idString, this.text.text);
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002252 File Offset: 0x00000452
	private void Start()
	{
		if (this.document != null)
		{
			this.text.text = this.document.FetchString(this.idInt, Language.English);
		}
	}

	// Token: 0x04000021 RID: 33
	public MultilingualTextDocument document;

	// Token: 0x04000022 RID: 34
	[TextLookup("document")]
	public string idString;

	// Token: 0x04000023 RID: 35
	[ReadOnly]
	public int idInt;

	// Token: 0x04000024 RID: 36
	[ReadOnly]
	public Text text;
}
