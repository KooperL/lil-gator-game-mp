using System;
using UnityEngine;

public class StringToPlayer : MonoBehaviour
{
	// Token: 0x06000FBC RID: 4028 RVA: 0x0000D9AF File Offset: 0x0000BBAF
	private void Awake()
	{
		this.positions = new Vector3[this.positionCount];
		this.line.positionCount = this.positionCount;
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x00052504 File Offset: 0x00050704
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

	public LineRenderer line;

	public int positionCount = 20;

	private Vector3[] positions;

	public float perlinVariance = 0.2f;
}
