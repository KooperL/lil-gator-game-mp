using System;
using UnityEngine;

public class FitBounds : MonoBehaviour
{
	// Token: 0x06000055 RID: 85 RVA: 0x00018760 File Offset: 0x00016960
	public void Fit(BoxCollider box)
	{
		base.transform.position = box.transform.TransformPoint(box.center);
		base.transform.rotation = box.transform.rotation;
		Vector3 vector = 0.5f * box.transform.TransformVector(box.size);
		ParticleSystem[] array = this.particles;
		for (int i = 0; i < array.Length; i++)
		{
			ParticleSystem.ShapeModule shape = array[i].shape;
			shape.shapeType = ParticleSystemShapeType.BoxShell;
			shape.position = Vector3.zero;
			shape.scale = vector;
		}
	}

	public ParticleSystem[] particles;
}
