using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
	// Token: 0x06000B27 RID: 2855 RVA: 0x00037B70 File Offset: 0x00035D70
	public ConditionalHideAttribute(string conditionalSourceField)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = false;
		this.Inverse = false;
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00037BC8 File Offset: 0x00035DC8
	public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = hideInInspector;
		this.Inverse = false;
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00037C20 File Offset: 0x00035E20
	public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00037C78 File Offset: 0x00035E78
	public ConditionalHideAttribute(bool hideInInspector = false)
	{
		this.ConditionalSourceField = "";
		this.HideInInspector = hideInInspector;
		this.Inverse = false;
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00037CD4 File Offset: 0x00035ED4
	public ConditionalHideAttribute(string[] conditionalSourceFields, bool[] conditionalSourceFieldInverseBools, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceFields = conditionalSourceFields;
		this.ConditionalSourceFieldInverseBools = conditionalSourceFieldInverseBools;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00037D34 File Offset: 0x00035F34
	public ConditionalHideAttribute(string[] conditionalSourceFields, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceFields = conditionalSourceFields;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	public string ConditionalSourceField = "";

	public string ConditionalSourceField2 = "";

	public string[] ConditionalSourceFields = new string[0];

	public bool[] ConditionalSourceFieldInverseBools = new bool[0];

	public bool HideInInspector;

	public bool Inverse;

	public bool UseOrLogic;

	public bool InverseCondition1;

	public bool InverseCondition2;
}
