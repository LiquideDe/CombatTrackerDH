using UnityEngine;
using System;

namespace CombarTracker
{
    public class CanDestroyView : MonoBehaviour
    {
        public event Action Close;

        public void DestroyView() => Destroy(gameObject);

        protected void ClosePressed() => Close?.Invoke();
    }
}

