using UnityEngine;
using Zenject;

namespace CombarTracker
{
    public class Bootstrap : MonoBehaviour
    {
        private LvlMediator _lvlMediator;
        private Creators _creators;

        [Inject]
        private void Construct(LvlMediator lvlMediator) 
        {
            _lvlMediator = lvlMediator;
        } 

        void Start()
        {
            _lvlMediator.ShowLoading();
        }

    }
}



