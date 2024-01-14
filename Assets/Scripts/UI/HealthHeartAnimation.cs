/* Heal animation uses transform of the bonus text UI
 * it's position supplied as an argument for DoHeal
 * 
 */
using UnityEngine;
using UnityEngine.UI;

enum HeartStates
{
     NormalState,
     DamageAnimState,
     HealAnimState
}
/// <summary>
/// All animations for health heart
/// </summary>
public class HealthHeartAnimation : MonoBehaviour
{
     [SerializeField] private Image _heartImage;
     [SerializeField] private Color _baseColor;
     [SerializeField] private Color _nearDeathColor;
     
     [SerializeField] private AnimationCurve _damageAnimCurve;
     [SerializeField] private float _damageAnimationSpeed;
     [SerializeField] private float _nearDeathFlashFrequency;
     private float _animationPhase = 0f;
     [SerializeField] private float _healAnimationSpeed;
     [SerializeField] private AnimationCurve _healScaleAnimCurve;
     [SerializeField] private AnimationCurve _healMoveAnimCurve;
     private HeartStates _animationState = HeartStates.NormalState;
     
     private bool _isNearDeath = false;
     private float _nearDeathPulseValue;
     
     private Vector3 _instancePersistentPosition;
     private Vector3 _healStartPos;
     
     private void Update()
     {
          if (_animationState == HeartStates.DamageAnimState)
          {
               PerformDamageAnimation();
          }
          else if(_animationState == HeartStates.HealAnimState)
          {
               PerformHealAnimation();
          }

          if (_isNearDeath)
          {
               PerformNearDeathAnimation();
          }
     }
     
     public void SaveInitialState(Vector3 currentPos, Vector3 parentPos, Vector3 healStartPos)
     {
          _instancePersistentPosition = currentPos;
          _healStartPos = healStartPos - parentPos;
     }
     
     public void EnableNearDeath()
     {
          _isNearDeath = true;
     }
     
     public void DisableNearDeath()
     {
          _isNearDeath = false;
          _heartImage.color = _baseColor;
     }
     
     public void StartDamageAnimation()
     {
          _animationState = HeartStates.DamageAnimState;
          _heartImage.color = _nearDeathColor;
          _animationPhase = 0f;
     }
     
     public void StartHealAnimation()
     {
          gameObject.SetActive(true);
          _animationPhase = 0;
          _animationState = HeartStates.HealAnimState;
     }
     
     private void PerformDamageAnimation()
     {
          transform.localScale = Vector3.one * _damageAnimCurve.Evaluate(_animationPhase);
          _animationPhase += _damageAnimationSpeed*Time.deltaTime;
          if (_animationPhase > 1)
          {
               gameObject.SetActive(false);
          }
     }

     private void PerformHealAnimation()
     {
          transform.localScale = Vector3.one * _healScaleAnimCurve.Evaluate(_animationPhase);
          transform.localPosition = Vector3.Lerp(_healStartPos, _instancePersistentPosition, _healMoveAnimCurve.Evaluate(_animationPhase));
          _animationPhase += _healAnimationSpeed*Time.deltaTime;
          if (_animationPhase > 1)
          {
               _animationState = HeartStates.NormalState;
               transform.localPosition = _instancePersistentPosition;
               _heartImage.color = _baseColor;    
          }
     }
     
     private void PerformNearDeathAnimation()
     {
          _nearDeathPulseValue = Mathf.Sin(Time.time * _nearDeathFlashFrequency) * 0.5f + 1;
          _heartImage.color = Color.Lerp(_baseColor, _nearDeathColor, _nearDeathPulseValue);
          transform.localScale = Vector3.one * (_nearDeathPulseValue*0.15f+0.85f);
     }
}
