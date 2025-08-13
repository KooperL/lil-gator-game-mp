using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D6 RID: 726
public class UIPlayerCoordinates : MonoBehaviour
{
	// Token: 0x06000F53 RID: 3923 RVA: 0x00049B2C File Offset: 0x00047D2C
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x00049B3C File Offset: 0x00047D3C
	private void Update()
	{
		Vector3 rawPosition = Player.RawPosition;
		Vector3 position = PlayerOrbitCamera.active.transform.position;
		Vector3 position2 = MainCamera.t.position;
		this.text.text = string.Format("Player({0:0}, {1:0}, {2:0})\nOrbitCamera({3:0}, {4:0}, {5:0})\nCamera({6:0}, {7:0}, {8:0})", new object[] { rawPosition.x, rawPosition.y, rawPosition.z, position.x, position.y, position.z, position2.x, position2.y, position2.z });
	}

	// Token: 0x0400142F RID: 5167
	private Text text;
}
