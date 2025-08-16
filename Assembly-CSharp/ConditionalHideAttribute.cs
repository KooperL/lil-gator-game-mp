using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
	// Token: 0x06000D3E RID: 3390 RVA: 0x0004A674 File Offset: 0x00048874
	public ConditionalHideAttribute(string conditionalSourceField)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = false;
		this.Inverse = false;
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0004A6CC File Offset: 0x000488CC
	public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = hideInInspector;
		this.Inverse = false;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0004A724 File Offset: 0x00048924
	public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceField = conditionalSourceField;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0004A77C File Offset: 0x0004897C
	public ConditionalHideAttribute(bool hideInInspector = false)
	{
		this.ConditionalSourceField = "";
		this.HideInInspector = hideInInspector;
		this.Inverse = false;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0004A7D8 File Offset: 0x000489D8
	public ConditionalHideAttribute(string[] conditionalSourceFields, bool[] conditionalSourceFieldInverseBools, bool hideInInspector, bool inverse)
	{
		this.ConditionalSourceFields = conditionalSourceFields;
		this.ConditionalSourceFieldInverseBools = conditionalSourceFieldInverseBools;
		this.HideInInspector = hideInInspector;
		this.Inverse = inverse;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0004A838 File Offset: 0x00048A38
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
