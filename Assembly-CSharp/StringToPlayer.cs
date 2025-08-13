using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class StringToPlayer : MonoBehaviour
{
	// Token: 0x06000CB4 RID: 3252 RVA: 0x0003D803 File Offset: 0x0003BA03
	private void Awake()
	{
		this.positions = new Vector3[this.positionCount];
		this.line.positionCount = this.positionCount;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0003D828 File Offset: 0x0003BA28
	private void LateUpdate()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = Player.Position;
		for (int i = 0; i < this.positionCount; i++)
		{
			float num = (float)i / ((float)this.positionCount - 1f);
			float num2 = Mathf.Sin(3.1415927f * num);
			this.positions[i] = Vector3.Lerp(position, position2, num);
			Vector3[] array = this.positions;
			int num3 = i;
			array[num3].x = array[num3].x + this.perlinVariance * num2 * (Mathf.PerlinNoise(this.positions[i].x, this.positions[i].y) - 0.5f);
			Vector3[] array2 = this.positions;
			int num4 = i;
			array2[num4].z = array2[num4].z + this.perlinVariance * num2 * (Mathf.PerlinNoise(this.positions[i].z, this.positions[i].y) - 0.5f);
		}
		this.line.SetPositions(this.positions);
	}

	// Token: 0x040010C9 RID: 4297
	public LineRenderer line;

	// Token: 0x040010CA RID: 4298
	public int positionCount = 20;

	// Token: 0x040010CB RID: 4299
	private Vector3[] positions;

	// Token: 0x040010CC RID: 4300
	public float perlinVariance = 0.2f;
}
