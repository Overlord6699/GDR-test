using System;
using UnityEngine;

namespace GDRTest.UI
{
    [RequireComponent(typeof(Animator))]
    public class UIAnimationContoller : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public void StartShowAnim()
        {
            _animator.SetTrigger("Show");
        }

        public void StartHideAnim()
        {
            _animator.SetTrigger("Hide");
        }
    }
}
