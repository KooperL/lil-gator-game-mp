using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Cinemachine.Examples
{
	public class GenericTrigger : MonoBehaviour
	{
		// Token: 0x060013AD RID: 5037 RVA: 0x00010A46 File Offset: 0x0000EC46
		private void Start()
		{
			this.timeline = base.GetComponent<PlayableDirector>();
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00010A54 File Offset: 0x0000EC54
		private void OnTriggerExit(Collider c)
		{
			if (c.gameObject.CompareTag("Player"))
			{
				this.timeline.time = 27.0;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00010A7C File Offset: 0x0000EC7C
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
