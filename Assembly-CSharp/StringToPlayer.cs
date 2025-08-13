using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class StringToPlayer : MonoBehaviour
{
	// Token: 0x06000F60 RID: 3936 RVA: 0x0000D607 File Offset: 0x0000B807
	private void Awake()
	{
		this.positions = new Vector3[this.positionCount];
		this.line.positionCount = this.positionCount;
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000505E0 File Offset: 0x0004E7E0
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

	// Token: 0x040013E2 RID: 5090
	public LineRenderer line;

	// Token: 0x040013E3 RID: 5091
	public int positionCount = 20;

	// Token: 0x040013E4 RID: 5092
	private Vector3[] positions;

	// Token: 0x040013E5 RID: 5093
	public float perlinVariance = 0.2f;
}
