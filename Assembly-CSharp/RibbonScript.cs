using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class RibbonScript : MonoBehaviour
{
	// Token: 0x06000C1E RID: 3102 RVA: 0x00039B48 File Offset: 0x00037D48
	private void Start()
	{
		this.rootForceDirection.Normalize();
		int num = this.CountBones(base.transform);
		this.ribbon = new RibbonScript.RibbonPiece[num];
		this.PopulateRibbonArray(base.transform, 0);
		for (int i = 0; i < this.ribbon.Length; i++)
		{
			foreach (object obj in this.ribbon[i].bone.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.GetComponent<UnRibbon>() == null && transform.GetComponent<RibbonScript>() == null && transform.gameObject.name != "RIBBONROOT")
				{
					for (int j = 0; j < this.ribbon.Length; j++)
					{
						if (this.ribbon[j].bone.transform == transform)
						{
							this.ribbon[i].AddChild(j);
							this.ribbon[j].parentIndex = i;
						}
					}
				}
				else if (transform.GetComponent<RibbonScript>() != null)
				{
					this.ribbon[i].AddChildRibbon(transform.GetComponent<RibbonScript>());
					transform.GetComponent<RibbonScript>().isCompleteRoot = false;
				}
				else if (transform.gameObject.name == "RIBBONROOT")
				{
					foreach (object obj2 in transform)
					{
						Transform transform2 = (Transform)obj2;
						if (transform2.GetComponent<RibbonScript>() != null)
						{
							this.ribbon[i].AddChildRibbon(transform2.GetComponent<RibbonScript>());
							transform2.GetComponent<RibbonScript>().isCompleteRoot = false;
						}
					}
				}
			}
		}
		this.ribbon[0].boneHolder = new GameObject();
		this.ribbon[0].boneHolder.name = "RIBBONROOT";
		this.ribbon[0].boneHolder.transform.position = this.ribbon[0].bone.transform.position;
		this.ribbon[0].boneHolder.transform.LookAt(this.ribbon[1].bone.transform.position);
		this.ribbon[0].boneHolder.transform.LookAt(this.ribbon[0].boneHolder.transform.position - this.ribbon[0].boneHolder.transform.forward);
		this.ribbon[0].boneHolder.transform.parent = base.transform.parent;
		this.ribbon[0].bone.transform.parent = this.ribbon[0].boneHolder.transform;
		for (int k = 1; k < this.ribbon.Length; k++)
		{
			this.ribbon[k].boneHolder = new GameObject();
			this.ribbon[k].boneHolder.name = string.Concat(new string[]
			{
				"RIBBON_",
				k.ToString(),
				"_",
				this.ribbon[0].bone.name,
				"& ",
				this.ribbon[k].parentIndex.ToString()
			});
			this.ribbon[k].boneHolder.transform.position = this.ribbon[k].bone.transform.position;
			this.ribbon[k].boneHolder.transform.LookAt(this.ribbon[this.ribbon[k].parentIndex].bone.transform.position);
			this.ribbon[k].boneHolder.transform.parent = this.ribbon[0].boneHolder.transform;
			this.ribbon[k].bone.transform.parent = this.ribbon[k].boneHolder.transform;
		}
		for (int l = 1; l < this.ribbon.Length; l++)
		{
			this.ribbon[l].linkLength = Vector3.Distance(this.ribbon[l].boneHolder.transform.position, this.ribbon[this.ribbon[l].parentIndex].boneHolder.transform.position);
		}
		this.initialised = true;
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0003A0CC File Offset: 0x000382CC
	private int CountBones(Transform parentLink)
	{
		int num = 1;
		foreach (object obj in parentLink)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<UnRibbon>() == null && transform.GetComponent<RibbonScript>() == null && transform.gameObject.name != "RIBBONROOT")
			{
				num += this.CountBones(transform);
			}
		}
		return num;
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0003A15C File Offset: 0x0003835C
	private int PopulateRibbonArray(Transform parentlink, int ribbonIndex)
	{
		this.ribbon[ribbonIndex].bone = parentlink.gameObject;
		this.ribbon[ribbonIndex].position = this.ribbon[ribbonIndex].bone.transform.position;
		ribbonIndex++;
		foreach (object obj in parentlink)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<UnRibbon>() == null && transform.GetComponent<RibbonScript>() == null && transform.gameObject.name != "RIBBONROOT")
			{
				ribbonIndex = this.PopulateRibbonArray(transform, ribbonIndex);
			}
		}
		return ribbonIndex;
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0003A230 File Offset: 0x00038430
	private void FixedUpdate()
	{
		if (this.isCompleteRoot)
		{
			this.RibbonAction();
		}
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x0003A240 File Offset: 0x00038440
	public void RibbonAction()
	{
		this.ActRibbonPiece(0, this.rootStrength);
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0003A250 File Offset: 0x00038450
	private void ActRibbonPiece(int ribbonIndex, float currentRootInfluence)
	{
		if (ribbonIndex > 0)
		{
			RibbonScript.RibbonPiece[] array = this.ribbon;
			array[ribbonIndex].position = array[ribbonIndex].position + this.ribbon[ribbonIndex].forces;
			if (Vector3.Distance(this.ribbon[this.ribbon[ribbonIndex].parentIndex].position, this.ribbon[ribbonIndex].position) > this.ribbon[ribbonIndex].linkLength)
			{
				Vector3 position = this.ribbon[ribbonIndex].position;
				Vector3 vector = this.ribbon[ribbonIndex].position - this.ribbon[this.ribbon[ribbonIndex].parentIndex].position;
				vector.Normalize();
				this.ribbon[ribbonIndex].position = this.ribbon[this.ribbon[ribbonIndex].parentIndex].position + vector * this.ribbon[ribbonIndex].linkLength;
				RibbonScript.RibbonPiece[] array2 = this.ribbon;
				array2[ribbonIndex].forces = array2[ribbonIndex].forces + (this.ribbon[ribbonIndex].position - position) * 0.8f;
			}
			RibbonScript.RibbonPiece[] array3 = this.ribbon;
			array3[ribbonIndex].forces = array3[ribbonIndex].forces * this.drag;
			RibbonScript.RibbonPiece[] array4 = this.ribbon;
			array4[ribbonIndex].forces = array4[ribbonIndex].forces + this.worldUp * -this.linkGravity;
			Vector3 vector2 = -this.ribbon[0].boneHolder.transform.right * this.rootForceDirection.x + -this.ribbon[0].boneHolder.transform.up * this.rootForceDirection.y + -this.ribbon[0].boneHolder.transform.forward * this.rootForceDirection.z;
			vector2.Normalize();
			RibbonScript.RibbonPiece[] array5 = this.ribbon;
			array5[ribbonIndex].forces = array5[ribbonIndex].forces + vector2 * currentRootInfluence;
			this.ribbon[ribbonIndex].forces = Vector3.ClampMagnitude(this.ribbon[ribbonIndex].forces, this.maxForceStrength);
		}
		else
		{
			this.ribbon[0].position = this.ribbon[0].boneHolder.transform.position;
		}
		if (this.ribbon[ribbonIndex].childIndeces != null)
		{
			for (int i = 0; i < this.ribbon[ribbonIndex].childIndeces.Length; i++)
			{
				this.ActRibbonPiece(this.ribbon[ribbonIndex].childIndeces[i], currentRootInfluence * this.rootInfluence);
			}
		}
		if (this.ribbon[ribbonIndex].childRibbons != null)
		{
			for (int j = 0; j < this.ribbon[ribbonIndex].childRibbons.Length; j++)
			{
				this.ribbon[ribbonIndex].childRibbons[j].RibbonAction();
			}
		}
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0003A5E4 File Offset: 0x000387E4
	private void LateUpdate()
	{
		if (this.isCompleteRoot)
		{
			Vector3 vector = this.ribbon[0].boneHolder.transform.position - this.ribbon[0].position;
			this.RibbonPlacement(vector);
		}
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0003A632 File Offset: 0x00038832
	public void RibbonPlacement(Vector3 intervalOffset)
	{
		this.PlaceRibbonPiece(1, intervalOffset);
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0003A63C File Offset: 0x0003883C
	private void PlaceRibbonPiece(int ribbonIndex, Vector3 intervalOffset)
	{
		this.ribbon[ribbonIndex].boneHolder.transform.position = this.ribbon[ribbonIndex].position + intervalOffset;
		this.ribbon[ribbonIndex].boneHolder.transform.LookAt(this.ribbon[this.ribbon[ribbonIndex].parentIndex].position + intervalOffset, this.ribbon[this.ribbon[ribbonIndex].parentIndex].boneHolder.transform.up);
		if (this.ribbon[ribbonIndex].childIndeces != null)
		{
			for (int i = 0; i < this.ribbon[ribbonIndex].childIndeces.Length; i++)
			{
				this.PlaceRibbonPiece(this.ribbon[ribbonIndex].childIndeces[i], intervalOffset);
			}
		}
		if (this.ribbon[ribbonIndex].childRibbons != null)
		{
			for (int j = 0; j < this.ribbon[ribbonIndex].childRibbons.Length; j++)
			{
				this.ribbon[ribbonIndex].childRibbons[j].RibbonPlacement(intervalOffset);
			}
		}
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0003A77D File Offset: 0x0003897D
	private void OnDrawGizmosSelected()
	{
		this.debuRootInfluence = 1f;
		if (!this.initialised)
		{
			this.DrawHierachy(base.transform);
			return;
		}
		this.DrawRibbon(0, this.debuRootInfluence);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0003A7AC File Offset: 0x000389AC
	private void DrawHierachy(Transform targetTransform)
	{
		foreach (object obj in targetTransform.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<SkinnedMeshRenderer>() == null && transform.GetComponent<RibbonScript>() == null)
			{
				this.DrawBone(targetTransform.position, transform.position, this.debuRootInfluence);
				this.debuRootInfluence *= this.rootInfluence;
				this.DrawHierachy(transform);
			}
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0003A84C File Offset: 0x00038A4C
	private void DrawRibbon(int ribbonIndex, float influenceIndicator)
	{
		if (this.ribbon[ribbonIndex].childIndeces != null)
		{
			for (int i = 0; i < this.ribbon[ribbonIndex].childIndeces.Length; i++)
			{
				this.DrawBone(this.ribbon[ribbonIndex].position, this.ribbon[this.ribbon[ribbonIndex].childIndeces[i]].position, influenceIndicator);
				this.DrawRibbon(this.ribbon[ribbonIndex].childIndeces[i], influenceIndicator * this.rootInfluence);
			}
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0003A8E8 File Offset: 0x00038AE8
	private void DrawBone(Vector3 source, Vector3 child, float strengthness)
	{
		this.lineDirection = child - source;
		this.lineLength = this.lineDirection.magnitude;
		this.lineDirection.Normalize();
		this.crossingVector = new Vector3(this.lineDirection.z, this.lineDirection.x, this.lineDirection.y);
		this.theCross = Vector3.Cross(this.lineDirection, this.crossingVector);
		this.theCrossB = Vector3.Cross(this.lineDirection, this.theCross);
		this.bumpOffset = source + this.lineDirection * (this.lineLength * 0.2f);
		this.boneRadiusnuss = this.lineLength * this.boneRadius;
		this.boneColor.a = 1f;
		this.boneColor.r = 1f;
		this.boneColor.g = strengthness;
		this.boneColor.b = strengthness * -1f + 1f;
		Debug.DrawLine(source, this.bumpOffset + this.theCross * this.boneRadiusnuss, this.boneColor);
		Debug.DrawLine(source, this.bumpOffset + this.theCrossB * this.boneRadiusnuss, this.boneColor);
		Debug.DrawLine(source, this.bumpOffset - this.theCross * this.boneRadiusnuss, this.boneColor);
		Debug.DrawLine(source, this.bumpOffset - this.theCrossB * this.boneRadiusnuss, this.boneColor);
		Debug.DrawLine(this.bumpOffset + this.theCross * this.boneRadiusnuss, child, this.boneColor);
		Debug.DrawLine(this.bumpOffset + this.theCrossB * this.boneRadiusnuss, child, this.boneColor);
		Debug.DrawLine(this.bumpOffset - this.theCross * this.boneRadiusnuss, child, this.boneColor);
		Debug.DrawLine(this.bumpOffset - this.theCrossB * this.boneRadiusnuss, child, this.boneColor);
	}

	// Token: 0x04000FE0 RID: 4064
	public float linkGravity = 0.015f;

	// Token: 0x04000FE1 RID: 4065
	public float maxForceStrength = 0.8f;

	// Token: 0x04000FE2 RID: 4066
	public float drag = 0.9f;

	// Token: 0x04000FE3 RID: 4067
	public Vector3 rootForceDirection = new Vector3(0f, 0f, 1f);

	// Token: 0x04000FE4 RID: 4068
	public float rootStrength = 0.05f;

	// Token: 0x04000FE5 RID: 4069
	public float rootInfluence = 0.4f;

	// Token: 0x04000FE6 RID: 4070
	private Vector3 worldUp = Vector3.up;

	// Token: 0x04000FE7 RID: 4071
	private RibbonScript.RibbonPiece[] ribbon;

	// Token: 0x04000FE8 RID: 4072
	[HideInInspector]
	public bool isCompleteRoot = true;

	// Token: 0x04000FE9 RID: 4073
	private bool initialised;

	// Token: 0x04000FEA RID: 4074
	private float boneRadius = 0.2f;

	// Token: 0x04000FEB RID: 4075
	private Vector3 lineDirection;

	// Token: 0x04000FEC RID: 4076
	private Vector3 bumpOffset;

	// Token: 0x04000FED RID: 4077
	private Vector3 crossingVector;

	// Token: 0x04000FEE RID: 4078
	private Vector3 theCross;

	// Token: 0x04000FEF RID: 4079
	private Vector3 theCrossB;

	// Token: 0x04000FF0 RID: 4080
	private float lineLength;

	// Token: 0x04000FF1 RID: 4081
	private float boneRadiusnuss;

	// Token: 0x04000FF2 RID: 4082
	private float debuRootInfluence;

	// Token: 0x04000FF3 RID: 4083
	private Color boneColor;

	// Token: 0x0200041B RID: 1051
	private struct RibbonPiece
	{
		// Token: 0x06001AEC RID: 6892 RVA: 0x00072944 File Offset: 0x00070B44
		public void AddChild(int childInt)
		{
			if (this.childIndeces == null)
			{
				this.childIndeces = new int[0];
			}
			int[] array = new int[this.childIndeces.Length + 1];
			for (int i = 0; i < this.childIndeces.Length; i++)
			{
				array[i] = this.childIndeces[i];
			}
			array[this.childIndeces.Length] = childInt;
			this.childIndeces = new int[array.Length];
			for (int j = 0; j < this.childIndeces.Length; j++)
			{
				this.childIndeces[j] = array[j];
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000729CC File Offset: 0x00070BCC
		public void AddChildRibbon(RibbonScript childScript)
		{
			if (this.childRibbons == null)
			{
				this.childRibbons = new RibbonScript[0];
			}
			RibbonScript[] array = new RibbonScript[this.childRibbons.Length + 1];
			for (int i = 0; i < this.childRibbons.Length; i++)
			{
				array[i] = this.childRibbons[i];
			}
			array[this.childRibbons.Length] = childScript;
			this.childRibbons = new RibbonScript[array.Length];
			for (int j = 0; j < this.childRibbons.Length; j++)
			{
				this.childRibbons[j] = array[j];
			}
		}

		// Token: 0x04001D25 RID: 7461
		public GameObject bone;

		// Token: 0x04001D26 RID: 7462
		public GameObject boneHolder;

		// Token: 0x04001D27 RID: 7463
		public Vector3 position;

		// Token: 0x04001D28 RID: 7464
		public Vector3 forces;

		// Token: 0x04001D29 RID: 7465
		public float linkLength;

		// Token: 0x04001D2A RID: 7466
		public int parentIndex;

		// Token: 0x04001D2B RID: 7467
		public int[] childIndeces;

		// Token: 0x04001D2C RID: 7468
		public RibbonScript[] childRibbons;
	}
}
