

namespace CombarTracker
{
    public class DamageItem : ItemInList
    {
        private int _place, _damage, _penetration;
        private bool _isWarp, _isIgnoreArmor, _isIgnoreToughness;
        public int Place => _place;
        public int Damage => _damage;
        public int Penetration => _penetration;
        public bool IsWarp => _isWarp;
        public bool IsIgnoreArmor => _isIgnoreArmor;
        public bool IsIgnoreToughness => _isIgnoreToughness;

        public void Initialize(string place, string damage, string penetration, bool isWarp, bool isIgnoreArmor, bool isIgnoreToughness)
        {
            base.Initialize(damage);
            gameObject.SetActive(true);
            int.TryParse(place, out _place);
            int.TryParse(damage, out _damage);
            int.TryParse(penetration, out _penetration);
            _isIgnoreArmor = isIgnoreArmor;
            _isIgnoreToughness = isIgnoreToughness;
            _isWarp = isWarp;
        }
    }
}

