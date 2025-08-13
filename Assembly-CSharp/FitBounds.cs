using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class FitBounds : MonoBehaviour
{
	// Token: 0x0600004D RID: 77 RVA: 0x00017F24 File Offset: 0x00016124
	public void Fit(BoxCollider box)
	{
		base.transform.position = box.transform.TransformPoint(box.center);
		base.transform.rotation = box.transform.rotation;
		Vector3 vector = 0.5f * box.transform.TransformVector(box.size);
		ParticleSystem[] array = this.particles;
		for (int i = 0; i < array.Length; i++)
		{
			ParticleSystem.ShapeModule shape = array[i].shape;
			shape.shapeType = 15;
			shape.position = Vector3.zero;
			shape.scale = vector;
		}
	}

	// Token: 0x04000069 RID: 105
	public ParticleSystem[] particles;
}
