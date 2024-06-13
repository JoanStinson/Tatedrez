﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace JGM.Game
{
    [AddComponentMenu("UI/TextMeshProAnimatedBinder")]
    public class TextMeshProAnimatedBinder : MonoBehaviour
    {
        public event Action OnStartAnimation;

        [SerializeField] private TextMeshProUGUI m_text;
        [SerializeField] private float m_textAnimationDuration = 0.5f;
        [SerializeField] private Color m_addingAnimateColor;
        [SerializeField] private Color m_subtractingAnimateColor;

        private bool m_currentValueIsDifferentFromTarget => (m_currentValue != m_targetValue);

        private Coroutine m_animateCoroutine;
        private Color m_originalColor;
        private int m_startingValue;
        private int m_targetValue;
        private string m_prefixText = "";
        private int m_currentValue;
        private float m_textTimer;

        private void Awake()
        {
            m_originalColor = m_text.color;
        }

        public void SetValue(int value, string prefixText = "")
        {
            m_text.text = $"{prefixText}{value.ToString("0000")}";
        }

        public void SetValueAnimated(int value, string prefixText = "", float delay = 0)
        {
            m_textTimer = 0;
            m_startingValue = m_currentValue;
            m_targetValue = value;
            m_prefixText = prefixText;

            if (m_startingValue != m_targetValue)
            {
                if (m_animateCoroutine != null)
                {
                    StopCoroutine(m_animateCoroutine);
                }

                OnStartAnimation?.Invoke();
                m_animateCoroutine = StartCoroutine(Animate(delay));
            }
        }

        private IEnumerator Animate(float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            m_text.color = (m_currentValue < m_targetValue) ? m_addingAnimateColor : m_subtractingAnimateColor;

            while (m_currentValueIsDifferentFromTarget)
            {
                m_currentValue = (int)Mathf.Lerp(m_startingValue, m_targetValue, m_textTimer / m_textAnimationDuration);
                m_text.text = $"{m_prefixText}{m_currentValue.ToString("0000")}";
                m_textTimer += Time.deltaTime;
                yield return null;
            }

            m_text.text = $"{m_prefixText}{m_targetValue.ToString("0000")}";
            m_text.color = m_originalColor;
        }

        private void OnDisable()
        {
            if (m_currentValueIsDifferentFromTarget)
            {
                m_text.text = $"{m_prefixText}{m_targetValue.ToString("0000")}";
                m_text.color = m_originalColor;
            }
        }
    }
}