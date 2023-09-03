using UnityEngine;
using UnityEngine.Audio;

namespace Ui.MainMenu.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        public void ChangeAudioVolume(float volume) => _audioMixer.SetFloat("Audio", volume);
        
        public void ChangeMusicVolume(float volume) => _audioMixer.SetFloat("Music", volume);
        
        public void ChangeSoundVolume(float volume) => _audioMixer.SetFloat("Sound", volume);
    }
}