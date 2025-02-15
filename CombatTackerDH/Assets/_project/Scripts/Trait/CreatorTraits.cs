using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CombarTracker
{
    public class CreatorTraits
    {
        List<Trait> _traits = new List<Trait>();
        public List<Trait> Traits { get => _traits; }

        public CreatorTraits()
        {
            List<string> dirs = new List<string>();
            dirs.AddRange(Directory.GetDirectories($"{Application.dataPath}/StreamingAssets/Features"));
            foreach (string path in dirs)
            {
                _traits.Add(new Trait(GameStat.ReadText(path + "/Название.txt"), GameStat.ReadText(path + "/Описание.txt")));
            }
        }

        public Trait GetFeature(string name)
        {
            foreach (Trait feature in _traits)
            {
                if (string.Compare(name, feature.Name, true) == 0)
                    return feature;
            }

            Debug.Log($"Не смогли найти trait {name}");
            return null;
        }
    }
}

