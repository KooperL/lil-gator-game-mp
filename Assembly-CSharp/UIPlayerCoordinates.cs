using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerCoordinates : MonoBehaviour
{
	// Token: 0x060012C7 RID: 4807 RVA: 0x0000FD00 File Offset: 0x0000DF00
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0005CF84 File Offset: 0x0005B184
	private void Update()
	{
		Vector3 rawPosition = Player.RawPosition;
		Vector3 position = PlayerOrbitCamera.active.transform.position;
		Vector3 position2 = MainCamera.t.position;
		this.text.text = string.Format("Player({0:0}, {1:0}, {2:0})\nOrbitCamera({3:0}, {4:0}, {5:0})\nCamera({6:0}, {7:0}, {8:0})", new object[] { rawPosition.x, rawPosition.y, rawPosition.z, position.x, position.y, position.z, position2.x, position2.y, position2.z });
	}

	private Text text;
}
