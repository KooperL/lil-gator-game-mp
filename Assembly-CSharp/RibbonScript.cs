using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class RibbonScript : MonoBehaviour
{
	// Token: 0x06000EBE RID: 3774 RVA: 0x0004D088 File Offset: 0x0004B288
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

	// Token: 0x06000EBF RID: 3775 RVA: 0x0004D60C File Offset: 0x0004B80C
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

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0004D69C File Offset: 0x0004B89C
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

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0000CE7A File Offset: 0x0000B07A
	private void FixedUpdate()
	{
		if (this.isCompleteRoot)
		{
			this.RibbonAction();
		}
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0000CE8A File Offset: 0x0000B08A
	public void RibbonAction()
	{
		this.ActRibbonPiece(0, this.rootStrength);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0004D770 File Offset: 0x0004B970
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

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0004DB04 File Offset: 0x0004BD04
	private void LateUpdate()
	{
		if (this.isCompleteRoot)
		{
			Vector3 vector = this.ribbon[0].boneHolder.transform.position - this.ribbon[0].position;
			this.RibbonPlacement(vector);
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0000CE99 File Offset: 0x0000B099
	public void RibbonPlacement(Vector3 intervalOffset)
	{
		this.PlaceRibbonPiece(1, intervalOffset);
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0004DB54 File Offset: 0x0004BD54
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

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0000CEA3 File Offset: 0x0000B0A3
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

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0004DC98 File Offset: 0x0004BE98
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

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0004DD38 File Offset: 0x0004BF38
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

	// Token: 0x06000ECA RID: 3786 RVA: 0x0004DDD4 File Offset: 0x0004BFD4
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

	// Token: 0x040012D4 RID: 4820
	public float linkGravity = 0.015f;

	// Token: 0x040012D5 RID: 4821
	public float maxForceStrength = 0.8f;

	// Token: 0x040012D6 RID: 4822
	public float drag = 0.9f;

	// Token: 0x040012D7 RID: 4823
	public Vector3 rootForceDirection = new Vector3(0f, 0f, 1f);

	// Token: 0x040012D8 RID: 4824
	public float rootStrength = 0.05f;

	// Token: 0x040012D9 RID: 4825
	public float rootInfluence = 0.4f;

	// Token: 0x040012DA RID: 4826
	private Vector3 worldUp = Vector3.up;

	// Token: 0x040012DB RID: 4827
	private RibbonScript.RibbonPiece[] ribbon;

	// Token: 0x040012DC RID: 4828
	[HideInInspector]
	public bool isCompleteRoot = true;

	// Token: 0x040012DD RID: 4829
	private bool initialised;

	// Token: 0x040012DE RID: 4830
	private float boneRadius = 0.2f;

	// Token: 0x040012DF RID: 4831
	private Vector3 lineDirection;

	// Token: 0x040012E0 RID: 4832
	private Vector3 bumpOffset;

	// Token: 0x040012E1 RID: 4833
	private Vector3 crossingVector;

	// Token: 0x040012E2 RID: 4834
	private Vector3 theCross;

	// Token: 0x040012E3 RID: 4835
	private Vector3 theCrossB;

	// Token: 0x040012E4 RID: 4836
	private float lineLength;

	// Token: 0x040012E5 RID: 4837
	private float boneRadiusnuss;

	// Token: 0x040012E6 RID: 4838
	private float debuRootInfluence;

	// Token: 0x040012E7 RID: 4839
	private Color boneColor;

	// Token: 0x020002EF RID: 751
	private struct RibbonPiece
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
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

		// Token: 0x06000ECD RID: 3789 RVA: 0x0004E12C File Offset: 0x0004C32C
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

		// Token: 0x040012E8 RID: 4840
		public GameObject bone;

		// Token: 0x040012E9 RID: 4841
		public GameObject boneHolder;

		// Token: 0x040012EA RID: 4842
		public Vector3 position;

		// Token: 0x040012EB RID: 4843
		public Vector3 forces;

		// Token: 0x040012EC RID: 4844
		public float linkLength;

		// Token: 0x040012ED RID: 4845
		public int parentIndex;

		// Token: 0x040012EE RID: 4846
		public int[] childIndeces;

		// Token: 0x040012EF RID: 4847
		public RibbonScript[] childRibbons;
	}
}
