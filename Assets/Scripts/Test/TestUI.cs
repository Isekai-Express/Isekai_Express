using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace Test
{
    public class TestUI:MonoBehaviour
    {
        [SerializeField] private Vector2VariableSO playerInput;
        [SerializeField] private Vector3VariableSO playerVelocity;
        
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI velocityText;
        [SerializeField] private TextMeshProUGUI inputText;

        public void Start()
        {
            playerInput.OnValueChanged += OnPlayerInputChanged;
            playerVelocity.OnValueChanged += OnPlayerVelocityChanged;
        }
        
        private void OnPlayerInputChanged(Vector2 value)
        {
            inputText.text = $"Input: {value}";
        }
        
        private void OnPlayerVelocityChanged(Vector3 value)
        {
            velocityText.text = $"Velocity: {value}";
            speedText.text = $"Speed: {value.magnitude}";
        }
    }
}