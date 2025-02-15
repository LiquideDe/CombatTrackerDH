
namespace CombarTracker
{
    public class Trait : IName
    {
        private string _name;
        private int _lvl;
        private string _description;


        public string Name => _name;
        public string NameWithNumber => GetNameWithNumber();
        public int Lvl { get => _lvl; set => _lvl = value; }
        public string Description => _description;

        public Trait(string name, int lvl = 0)
        {
            _name = name;
            _lvl = lvl;
        }

        public Trait(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public Trait(Trait trait)
        {
            _name = trait.Name;
            _description = trait.Description;
            _lvl = trait.Lvl;
        }

        private string GetNameWithNumber()
        {
            if (_lvl > 0)
                return $"{_name}({_lvl})";
            else
                return _name;
        }
    }
}

