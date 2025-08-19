using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MLText : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00017C78 File Offset: 0x00015E78
	public static void ForceRefresh()
	{
		foreach (MLText mltext in MLText.activeMLText)
		{
			mltext.Refresh();
		}
		SelectOptions.ForceRefresh();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00017CCC File Offset: 0x00015ECC
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

	// Token: 0x06000028 RID: 40 RVA: 0x00017D9C File Offset: 0x00015F9C
	[ContextMenu("Add entry to document")]
	public void AddEntryToDocument()
	{
		if (this.document != null && this.text != null && !this.document.HasString(this.idString))
		{
			MLTextUtil.AddMLStringEntry(this.document, this.idString, this.text.text);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002252 File Offset: 0x00000452
	private void OnEnable()
	{
		this.Refresh();
		MLText.activeMLText.Add(this);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002265 File Offset: 0x00000465
	private void OnDisable()
	{
		MLText.activeMLText.Remove(this);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002273 File Offset: 0x00000473
	public void Refresh()
	{
		if (this.document != null)
		{
			this.text.text = this.document.FetchString(this.idInt, Language.Auto);
		}
	}

	public static List<MLText> activeMLText = new List<MLText>();

	public MultilingualTextDocument document;

	[TextLookup("document")]
	public string idString;

	[ReadOnly]
	public int idInt;

	[ReadOnly]
	public Text text;
}
