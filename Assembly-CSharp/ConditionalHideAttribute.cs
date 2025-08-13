using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
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

	// Token: 0x04000EED RID: 3821
	public string ConditionalSourceField = "";

	// Token: 0x04000EEE RID: 3822
	public string ConditionalSourceField2 = "";

	// Token: 0x04000EEF RID: 3823
	public string[] ConditionalSourceFields = new string[0];

	// Token: 0x04000EF0 RID: 3824
	public bool[] ConditionalSourceFieldInverseBools = new bool[0];

	// Token: 0x04000EF1 RID: 3825
	public bool HideInInspector;

	// Token: 0x04000EF2 RID: 3826
	public bool Inverse;

	// Token: 0x04000EF3 RID: 3827
	public bool UseOrLogic;

	// Token: 0x04000EF4 RID: 3828
	public bool InverseCondition1;

	// Token: 0x04000EF5 RID: 3829
	public bool InverseCondition2;
}
