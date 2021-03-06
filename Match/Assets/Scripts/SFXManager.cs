using UnityEngine;

public enum Clip { Theme, StartGame, Select, Hover, Cut, Match, Punch };

public class SFXManager : MonoBehaviour {
	public static SFXManager instance;

	private AudioSource[] sfx;

	void Start () {
		instance = GetComponent<SFXManager>();
		sfx = GetComponents<AudioSource>();
    }

	public void PlaySFX(Clip audioClip) {
		sfx[(int)audioClip].Play();
	}
}
