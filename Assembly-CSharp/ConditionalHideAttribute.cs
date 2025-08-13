using System;
using UnityEngine;

// Token: 0x02000297 RID: 663
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
	// Token: 0x06000CF2 RID: 3314 RVA: 0x00048C80 File Offset: 0x00046E80
	public ConditionalHideAttribute(string conditionalSourceField)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = false;
		this.Inverse = false;
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00048CD8 File Offset: 0x00046ED8
	public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = hideInInspector;
		this.Inverse = false;
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x00048D30 File Offset: 0x00046F30
	public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x00048D88 File Offset: 0x00046F88
	public ConditionalHideAttribute(bool hideInInspector = false)
	{
		this.ConditionalSourceField = "";
		this.HideInInspector = hideInInspector;
		this.Inverse = false;
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x00048DE4 File Offset: 0x00046FE4
	public ConditionalHideAttribute(string[] conditionalSourceFields, bool[] conditionalSourceFieldInverseBools, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceFields = conditionalSourceFields;
		this.ConditionalSourceFieldInverseBools = conditionalSourceFieldInverseBools;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00048E44 File Offset: 0x00047044
	public ConditionalHideAttribute(string[] conditionalSourceFields, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceFields = conditionalSourceFields;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x04001153 RID: 4435
	public string ConditionalSourceField = "";

	// Token: 0x04001154 RID: 4436
	public string ConditionalSourceField2 = "";

	// Token: 0x04001155 RID: 4437
	public string[] ConditionalSourceFields = new string[0];

	// Token: 0x04001156 RID: 4438
	public bool[] ConditionalSourceFieldInverseBools = new bool[0];

	// Token: 0x04001157 RID: 4439
	public bool HideInInspector;

	// Token: 0x04001158 RID: 4440
	public bool Inverse;

	// Token: 0x04001159 RID: 4441
	public bool UseOrLogic;

	// Token: 0x0400115A RID: 4442
	public bool InverseCondition1;

	// Token: 0x0400115B RID: 4443
	public bool InverseCondition2;
}
