using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class PersistentAudioListener : MonoBehaviour
{
	// Token: 0x06000993 RID: 2451 RVA: 0x0000945B File Offset: 0x0000765B
	private void Start()
	{
		if (PersistentAudioListener.persistentInstance == null)
		{
			PersistentAudioListener.persistentInstance = this;
			this.audioListener.enabled = true;
			Object.DontDestroyOnLoad(base.gameObject);
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00009493 File Offset: 0x00007693
	private void LateUpdate()
	{
		if (this.parent == null)
		{
			this.parent = MainCamera.t;
		}
		if (this.parent != null)
		{
			base.transform.position = this.parent.position;
		}
	}

	// Token: 0x04000C3C RID: 3132
	public static PersistentAudioListener persistentInstance;

	// Token: 0x04000C3D RID: 3133
	private Transform parent;

	// Token: 0x04000C3E RID: 3134
	public AudioListener audioListener;
}
