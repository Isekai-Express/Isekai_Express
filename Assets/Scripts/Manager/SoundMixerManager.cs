using UnityEngine;
using UnityEngine.Audio;

namespace Manager
{
    /// <summary>
    /// 사운드 믹서 관리자
    /// </summary>
    public class SoundMixerManager : MonoBehaviour
    {
        [SerializeField] AudioMixer audioMixer;
        

        /// <param name="volume"> 0.001f ~ 1f </param>
        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        }
        
        /// <param name="volume"> 0.001f ~ 1f </param>
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        }
        
        /// <param name="volume"> 0.001f ~ 1f </param>
        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20);
        }
    }
}