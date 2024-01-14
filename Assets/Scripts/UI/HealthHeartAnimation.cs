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

     private float _animationPhase = 0f;
     [SerializeField] private float _damageAnimationSpeed;
     [SerializeField] private float _nearDeathFlashFrequency;
     private float _nearDeathPulse;
     [SerializeField] private float _healAnimationSpeed;
     [SerializeField] private AnimationCurve _healScaleAnimCurve;
     [SerializeField] private AnimationCurve _healMoveAnimCurve;
     private Vector3 _instancePersistentPosition;
     private Vector3 _healStartPos;
     private HeartStates _heartState = HeartStates.NormalState;
     private bool _isNearDeath = false;

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
     
     public void DoDamage()
     {
          _heartState = HeartStates.DamageAnimState;
          _heartImage.color = _nearDeathColor;
          _animationPhase = 0f;
     }
     
     private void DamageAnimationState()
     {
          transform.localScale = Vector3.one * _damageAnimCurve.Evaluate(_animationPhase);
          _animationPhase += _damageAnimationSpeed*Time.deltaTime;
          if (_animationPhase > 1)
          {
               gameObject.SetActive(false);
          }
     }

     public void DoHeal()
     {
          gameObject.SetActive(true);
          _animationPhase = 0;
          _heartState = HeartStates.HealAnimState;
     }
     
     private void HealAnimationState()
     {
          transform.localScale = Vector3.one * _healScaleAnimCurve.Evaluate(_animationPhase);
          transform.localPosition = Vector3.Lerp(_healStartPos, _instancePersistentPosition, _healMoveAnimCurve.Evaluate(_animationPhase));
          _animationPhase += _healAnimationSpeed*Time.deltaTime;
          if (_animationPhase > 1)
          {
               _heartState = HeartStates.NormalState;
               transform.localPosition = _instancePersistentPosition;
               _heartImage.color = _baseColor;    
          }
     }

     private void AnimateNearDeath()
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
          else if(_heartState == HeartStates.HealAnimState)
          {
               HealAnimationState();
          }

          if (_isNearDeath)
          {
               AnimateNearDeath();
          }
          
     }
}
