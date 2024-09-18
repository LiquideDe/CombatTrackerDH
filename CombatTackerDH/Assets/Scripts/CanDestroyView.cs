using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanDestroyView : MonoBehaviour
{
    public event Action Close;

    public void DestroyView() => Destroy(gameObject);

    protected void ClosePressed() => Close?.Invoke();
}
