using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasMesh : Graphic
{
	// Token: 0x06000E63 RID: 3683 RVA: 0x00044CE0 File Offset: 0x00042EE0
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		if (this.Mesh == null)
		{
			return;
		}
		Vector3[] vertices = this.Mesh.vertices;
		Vector2[] array = this.Mesh.uv;
		if (array.Length < vertices.Length)
		{
			array = new Vector2[vertices.Length];
		}
		Vector2 vector = this.Mesh.bounds.min;
		Vector2 vector2 = this.Mesh.bounds.size;
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector2 vector3 = vertices[i];
			vector3.x = (vector3.x - vector.x) / vector2.x;
			vector3.y = (vector3.y - vector.y) / vector2.y;
			vector3 = Vector2.Scale(vector3 - base.rectTransform.pivot, base.rectTransform.rect.size);
			vh.AddVert(vector3, this.color, array[i]);
		}
		int[] triangles = this.Mesh.triangles;
		for (int j = 0; j < triangles.Length; j += 3)
		{
			vh.AddTriangle(triangles[j], triangles[j + 1], triangles[j + 2]);
		}
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00044E4C File Offset: 0x0004304C
	public Vector3 TransformVertex(Vector3 vertex)
	{
		Vector2 vector;
		vector.x = (vertex.x - this.Mesh.bounds.min.x) / this.Mesh.bounds.size.x;
		vector.y = (vertex.y - this.Mesh.bounds.min.y) / this.Mesh.bounds.size.y;
		vector = Vector2.Scale(vector - base.rectTransform.pivot, base.rectTransform.rect.size);
		return base.transform.TransformPoint(vector);
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x00044F14 File Offset: 0x00043114
	public Vector3 InverseTransformVertex(Vector3 vertex)
	{
		Vector2 vector = base.transform.InverseTransformPoint(vertex);
		vector.x /= base.rectTransform.rect.size.x;
		vector.y /= base.rectTransform.rect.size.y;
		vector += base.rectTransform.pivot;
		vector = Vector2.Scale(vector, this.Mesh.bounds.size);
		vector.x += this.Mesh.bounds.min.x;
		vector.y += this.Mesh.bounds.min.y;
		return vector;
	}

	public Mesh Mesh;
}
