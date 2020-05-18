using System.Collections;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager> { //Self Containmen

	public ParticleSystem[] particles;

	public void PlayEffect (int num) {
		particles[num].Play (true); //ParticleSystemアニメーションを表示
	}
}