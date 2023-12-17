using System;
using UnityEngine;
using UnityEngine.UI;

enum HeartStates
{
     NormalState,
     DamageAnimState,
     HealAnimState,
     NearDeathAnimState
}

public class HealthHeartAnimation : MonoBehaviour
{
     [SerializeField] private Image _heartImage;
     [SerializeField] private Color _baseColor;
     [SerializeField] private Color _healColor;
     [SerializeField] private Color _nearDeathColor;
     [SerializeField] private AnimationCurve _damageAnimCurve;

     private float _damageAnimationPhase = 0f;
     [SerializeField] private float _damageAnimationSpeed;
     [SerializeField] private float _nearDeathFlashFrequency;
     private float _nearDeathPulse;
     

     private HeartStates _heartState = HeartStates.NormalState;

     public void DoDamage()
     {
          Debug.Log("Damaged");
          _heartState = HeartStates.DamageAnimState;
          _heartImage.color = _nearDeathColor;
     }

     private void DamageAnimationState()
     {
          transform.localScale = Vector3.one * _damageAnimCurve.Evaluate(_damageAnimationPhase);
          _damageAnimationPhase += _damageAnimationSpeed*Time.deltaTime;
          if (_damageAnimationPhase > 1)
          {
               this.gameObject.SetActive(false);
          }
     }

     public void DoHeal()
     {
          Debug.Log("Healed");
     }

     public void DoNearDeath()
     {
          Debug.Log("Near Death");
          _heartState = HeartStates.NearDeathAnimState;
     }

     private void NearDeathState()
     {
          _nearDeathPulse = Mathf.Sin(Time.time * _nearDeathFlashFrequency) * 0.5f + 1;
          _heartImage.color = Color.Lerp(_baseColor, _nearDeathColor, _nearDeathPulse);
          transform.localScale = Vector3.one * (_nearDeathPulse*0.15f+0.85f);
     }
     private void Update()
     {
          if (_heartState == HeartStates.DamageAnimState)
          {
               DamageAnimationState();
          }
          else if(_heartState == HeartStates.NearDeathAnimState)
          {
               NearDeathState();
          }
     }
}
