using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Cinemachine.Examples
{
	// Token: 0x020003E7 RID: 999
	public class GenericTrigger : MonoBehaviour
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x00010649 File Offset: 0x0000E849
		private void Start()
		{
			this.timeline = base.GetComponent<PlayableDirector>();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00010657 File Offset: 0x0000E857
		private void OnTriggerExit(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.time = 27.0;
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0001067F File Offset: 0x0000E87F
		private void OnTriggerEnter(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.Stop();
				this.timeline.Play();
			}
		}

		// Token: 0x040018E5 RID: 6373
		public PlayableDirector timeline;
	}
}
