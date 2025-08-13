using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class FitBounds : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003524 File Offset: 0x00001724
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

	// Token: 0x0400007F RID: 127
	public ParticleSystem[] particles;
}
