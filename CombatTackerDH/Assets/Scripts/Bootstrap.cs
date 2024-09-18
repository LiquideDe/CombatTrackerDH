using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private LvlMediator _lvlMediator;

    [Inject]
    private void Construct(LvlMediator lvlMediator) => _lvlMediator = lvlMediator;

    // Start is called before the first frame update
    void Start()
    {
        _lvlMediator.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
