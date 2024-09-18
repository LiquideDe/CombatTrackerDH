using UnityEngine;
using TMPro;

public class NewArmor : CreatorNewEquipment
{
    [SerializeField] private TMP_InputField inputArmorPoint, inputHeadPoint, inputHandsPoint, inputBodyPoint, inputLegsPoint, inputMaxAgility, inputProp;

    public override void FinishCreating()
    {
        if (_inputName.text.Length > 0 && _inputWeight.text.Length > 0)
        {
            int.TryParse(inputArmorPoint.text, out int armor);
            int.TryParse(inputHeadPoint.text, out int head);
            int.TryParse(inputHandsPoint.text, out int hands);
            int.TryParse(inputBodyPoint.text, out int body);
            int.TryParse(inputLegsPoint.text, out int legs);
            int.TryParse(inputMaxAgility.text, out int maxAgility);
            float.TryParse(_inputWeight.text, out float weight);

            JSONArmorReader armorReader = new JSONArmorReader();
            armorReader.body = body;
            armorReader.hands = hands;
            armorReader.head = head;
            armorReader.legs = legs;
            armorReader.maxAgility = maxAgility;
            armorReader.name = _inputName.text;
            armorReader.weight = weight;
            armorReader.armorPoint = armor;
            armorReader.description = "";
            armorReader.rarity = _inputRarity.text;
            armorReader.descriptionArmor = _inputDescription.text;
            if (head > 0 && hands > 0 && body > 0 && legs > 0)
            {
                armorReader.description += $"��������� ��� ����.";
            }
            else if (head == 0 && hands > 0 && body > 0 && legs > 0)
            {
                armorReader.description += $"��������� ��� ���� ����� ������.";
            }
            else if (head == 0 && hands == 0 && body > 0 && legs > 0)
            {
                armorReader.description += $"��������� ������ ���� � ����.";
            }
            else if (head == 0 && hands == 0 && body > 0 && legs == 0)
            {
                armorReader.description += $"��������� ������ ����.";
            }
            else if (head == 0 && hands > 0 && body > 0 && legs == 0)
            {
                armorReader.description += $"��������� ������ ���� � ����.";
            }
            else if (head == 0 && hands > 0 && body == 0 && legs == 0)
            {
                armorReader.description += $"��������� ������ ����.";
            }
            else if (head == 0 && hands == 0 && body == 0 && legs > 0)
            {
                armorReader.description += $"��������� ������ ����.";
            }
            else if (head > 0 && hands == 0 && body == 0 && legs == 0)
            {
                armorReader.description += $"��������� ������ ������.";
            }

            armorReader.typeEquipment = Equipment.TypeEquipment.Armor.ToString();
            armorReader.description += $"����� {armor}, ������������ �������� {maxAgility}.";
            armorReader.amount = 1;
            Armor armorEq = new Armor(armorReader);
            SaveEquipment($"{Application.dataPath}/StreamingAssets/Equipments/Armor/{armorReader.name}.JSON", armorReader);
            Debug.Log($"��������� {armorReader.name}");
            SendEquipment(armorEq);
        }
        else
            WrongInputPressed();
    }
}
