using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour {

  
        // Синглтон
        public static SpecialEffectsHelper Instance;

        ParticleSystem smokeEffect;
   

        void Awake()
        {
        smokeEffect = Resources.Load<ParticleSystem>("ShockFlash");
        // регистрация синглтона
        if (Instance != null)
            {
                Debug.LogError("Несколько экземпляров SpecialEffectsHelper!");
            }

            Instance = this;
        }

        // Создать взрыв в данной точке
        public void Explosion(Vector3 position)
        {
            // Взрыв
            instantiate(smokeEffect, position);
    
        }

        // Создание экземпляра системы частиц из префаба
        private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
        {
            ParticleSystem newParticleSystem = Instantiate(
              prefab,
              position,
              Quaternion.identity
            ) as ParticleSystem;

            // Убедитесь, что это будет уничтожено
            Destroy(
              newParticleSystem.gameObject,
              newParticleSystem.startLifetime
            );

            return newParticleSystem;
        }
    }

