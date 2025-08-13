using System;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class PersistentAudioListener : MonoBehaviour
{
	// Token: 0x06000824 RID: 2084 RVA: 0x000270A2 File Offset: 0x000252A2
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

	// Token: 0x06000825 RID: 2085 RVA: 0x000270DA File Offset: 0x000252DA
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

	// Token: 0x04000A50 RID: 2640
	public static PersistentAudioListener persistentInstance;

	// Token: 0x04000A51 RID: 2641
	private Transform parent;

	// Token: 0x04000A52 RID: 2642
	public AudioListener audioListener;
}
