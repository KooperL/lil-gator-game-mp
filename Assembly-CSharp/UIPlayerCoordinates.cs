using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003C2 RID: 962
public class UIPlayerCoordinates : MonoBehaviour
{
	// Token: 0x06001267 RID: 4711 RVA: 0x0000F91F File Offset: 0x0000DB1F
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x0005B168 File Offset: 0x00059368
	private void Update()
	{
		Vector3 rawPosition = Player.RawPosition;
		Vector3 position = PlayerOrbitCamera.active.transform.position;
		Vector3 position2 = MainCamera.t.position;
		this.text.text = string.Format("Player({0:0}, {1:0}, {2:0})\nOrbitCamera({3:0}, {4:0}, {5:0})\nCamera({6:0}, {7:0}, {8:0})", new object[] { rawPosition.x, rawPosition.y, rawPosition.z, position.x, position.y, position.z, position2.x, position2.y, position2.z });
	}

	// Token: 0x040017D4 RID: 6100
	private Text text;
}
