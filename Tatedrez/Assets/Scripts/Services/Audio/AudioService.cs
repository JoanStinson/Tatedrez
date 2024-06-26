﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public sealed class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private Transform m_poolParent;
        [SerializeField, Range(1, 20)] private int m_maxSimultaneousAudioSources = 10;
        [SerializeField, Range(0, 1)] private float m_musicVolume = 1f;

        [Inject] private AudioLibrary m_audioAssets;
        [Inject] private ICoroutineService m_coroutineService;

        private ComponentPool<AudioSource> m_pool;
        private Dictionary<string, AudioClip> m_audioLibrary;
        private AudioSource m_musicAudioSource;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_pool = new ComponentPool<AudioSource>(m_maxSimultaneousAudioSources, m_poolParent);
            m_audioLibrary = new Dictionary<string, AudioClip>();

            if (m_audioAssets != null)
            {
                foreach (var audioClip in m_audioAssets.Assets)
                {
                    m_audioLibrary.Add(audioClip.name, audioClip);
                }
            }
        }

        public void Play(string audioFileName, bool loop = false)
        {
            if (m_audioLibrary.TryGetValue(audioFileName, out var audioClip))
            {
                var audioSource = m_pool.Get();

                if (audioSource == null)
                {
                    Debug.LogWarning("No available audio source in the pool!");
                    return;
                }

                audioSource.clip = audioClip;
                audioSource.loop = loop;
                audioSource.Play();

                if (!loop)
                {
                    var coroutine = DisableAudioSourceAfterFinishedPlaying(audioSource, audioClip.length);
                    m_coroutineService.StartExternalCoroutine(coroutine);
                }
                else
                {
                    m_musicAudioSource = audioSource;
                    m_musicAudioSource.volume = m_musicVolume;
                }
            }
            else
            {
                Debug.LogWarning("Trying to play a clip that doesn't exist!");
            }
        }

        private IEnumerator DisableAudioSourceAfterFinishedPlaying(AudioSource audioSource, float duration)
        {
            yield return new WaitForSeconds(duration);
            m_pool.Return(audioSource);
        }

        public void SetDependencies(AudioLibrary audioLibrary, ICoroutineService coroutineService, ComponentPool<AudioSource> pool)
        {
            m_audioAssets = audioLibrary;
            m_coroutineService = coroutineService;
            m_pool = pool;
        }

        public void StopMusic()
        {
            m_musicAudioSource?.Stop();
        }
    }
}