using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Cinemachine.Examples
{
	public class GenericTrigger : MonoBehaviour
	{
		// Token: 0x060013AD RID: 5037 RVA: 0x00010A50 File Offset: 0x0000EC50
		private void Start()
		{
			this.timeline = base.GetComponent<PlayableDirector>();
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00010A5E File Offset: 0x0000EC5E
		private void OnTriggerExit(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.time = 27.0;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00010A86 File Offset: 0x0000EC86
		private void OnTriggerEnter(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.Stop();
				this.timeline.Play();
			}
		}

		public PlayableDirector timeline;
	}
}
