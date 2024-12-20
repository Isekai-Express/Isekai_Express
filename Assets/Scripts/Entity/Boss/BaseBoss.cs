using System;
using System.Runtime.Remoting.Messaging;
using DefaultNamespace;
using UnityEngine;

namespace Entity.Boss
{
    public class BaseBoss : Entity
    {
        [Header("Boss Properties")]
        [SerializeField]
        private float bossMaxHP;
        [SerializeField]
        private float bossHP;
        
        [Header("Inner References")]
        [SerializeField]
        private Animator animator;
        [Header("Outer References")]
        [SerializeField]
        private FloatVariableSO playerHP;
        [SerializeField]
        private FloatVariableSO playerMaxHP;
        
        
        private void OnEnable()
        {
            playerHP.OnValueChanged += OnPlayerHPChanged;
        }
        
        private void OnDisable()
        {
            playerHP.OnValueChanged -= OnPlayerHPChanged;
        }
        
        private void OnPlayerHPChanged(float value)
        {
            float playerHPratio = value / playerMaxHP.Value;
            animator.SetFloat("PlayerHPratio", playerHPratio);
        }
        
        public void TakeDamage(float damage)
        {
            bossHP -= damage;
            animator.SetFloat("BossHPratio", bossHP / bossMaxHP);
            if (bossHP <= 0)
            {
                // 죽음 처리
            }
        }
        
    }
}