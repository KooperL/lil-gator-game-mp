using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000E RID: 14
[RequireComponent(typeof(Text))]
public class MLText : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00002858 File Offset: 0x00000A58
	public static void ForceRefresh()
	{
		foreach (MLText mltext in MLText.activeMLText)
		{
			mltext.Refresh();
		}
		SelectOptions.ForceRefresh();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000028AC File Offset: 0x00000AAC
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

	// Token: 0x06000028 RID: 40 RVA: 0x0000297C File Offset: 0x00000B7C
	[ContextMenu("Add entry to document")]
	public void AddEntryToDocument()
	{
		if (this.document != null && this.text != null && !this.document.HasString(this.idString))
		{
			MLTextUtil.AddMLStringEntry(this.document, this.idString, this.text.text);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000029D4 File Offset: 0x00000BD4
	private void OnEnable()
	{
		this.Refresh();
		MLText.activeMLText.Add(this);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000029E7 File Offset: 0x00000BE7
	private void OnDisable()
	{
		MLText.activeMLText.Remove(this);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000029F5 File Offset: 0x00000BF5
	public void Refresh()
	{
		if (this.document != null)
		{
			this.text.text = this.document.FetchString(this.idInt, Language.Auto);
		}
	}

	// Token: 0x04000021 RID: 33
	public static List<MLText> activeMLText = new List<MLText>();

	// Token: 0x04000022 RID: 34
	public MultilingualTextDocument document;

	// Token: 0x04000023 RID: 35
	[TextLookup("document")]
	public string idString;

	// Token: 0x04000024 RID: 36
	[ReadOnly]
	public int idInt;

	// Token: 0x04000025 RID: 37
	[ReadOnly]
	public Text text;
}
