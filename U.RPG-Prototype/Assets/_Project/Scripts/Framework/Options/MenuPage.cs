﻿/*
 * MenuPage -
 * Created by : Allan N. Murillo
 * Last Edited : 7/10/2020
 */

using UnityEngine;
using System.Collections;

namespace ANM.Framework.Options
{
    public class MenuPage : MonoBehaviour
    {
        public static readonly string FlagOn = "ON";
        public static readonly string FlagOff = "OFF";
        public static readonly string FlagNone = "NONE";

        public string TargetState { get; private set; }
        public bool useAnimation;
        public MenuPageType type;

        private Animator _animator;
        private CanvasGroup _canvas;
        private static readonly int On = Animator.StringToHash("on");


        #region Unity Funcs

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
            if (!useAnimation) return;
            if (_animator != null) return;
            _animator = GetComponent<Animator>();
        }

        #endregion

        #region Public Funcs

        public void Animate(bool on)
        {
            if (useAnimation)
            {
                _animator.SetBool(On, on);
                //StopCoroutine($"AwaitAnimation");
                StartCoroutine(AwaitAnimation(on));
            }
            else
            {
                if (!on) TurnOff();
                else TurnOn();
            }
        }

        public void TurnOn()
        {
            //gameObject.SetActive(true);
            _canvas.alpha = 1f;
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
        }
        
        public void TurnOff()
        {
            _canvas.blocksRaycasts = false;
            _canvas.interactable = false;
            _canvas.alpha = 0f;
            //gameObject.SetActive(false);
        }

        #endregion

        #region Private Funcs

        private IEnumerator AwaitAnimation(bool on)
        {
            TargetState = on ? FlagOn : FlagOff;

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(TargetState)) yield return null;

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) yield return null;

            TargetState = FlagNone;

            Debug.Log("Finished Transition page to On ?: " + on);

            if (!on) TurnOff();
            else TurnOn();
        }

        #endregion
    }
}
