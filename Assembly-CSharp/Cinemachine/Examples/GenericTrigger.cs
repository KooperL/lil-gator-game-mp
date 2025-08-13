using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Cinemachine.Examples
{
	// Token: 0x020002F4 RID: 756
	public class GenericTrigger : MonoBehaviour
	{
		// Token: 0x06001020 RID: 4128 RVA: 0x0004D40F File Offset: 0x0004B60F
		private void Start()
		{
			this.timeline = base.GetComponent<PlayableDirector>();
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0004D41D File Offset: 0x0004B61D
		private void OnTriggerExit(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.time = 27.0;
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0004D445 File Offset: 0x0004B645
		private void OnTriggerEnter(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.Stop();
				this.timeline.Play();
			}
		}

		// Token: 0x04001522 RID: 5410
		public PlayableDirector timeline;
	}
}
