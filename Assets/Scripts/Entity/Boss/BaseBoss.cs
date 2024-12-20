using System;
using DefaultNamespace;
using UnityEngine;

namespace Entity.Boss
{
    public class BaseBoss : Entity
    {
        [SerializeField]
        private IntVariableSO playerHP;
        [SerializeField]
        private IntVariableSO playerMaxHP;
        [SerializeField]
        private Animator animator;
        
        private void OnEnable()
        {
            playerHP.OnValueChanged += OnPlayerHPChanged;
        }
        
        private void OnDisable()
        {
            playerHP.OnValueChanged -= OnPlayerHPChanged;
        }
        
        private void OnPlayerHPChanged(int value)
        {
            float playerHPratio = (float) value / playerMaxHP.Value;
            animator.SetFloat("PlayerHPratio", playerHPratio);
        }
    }
}