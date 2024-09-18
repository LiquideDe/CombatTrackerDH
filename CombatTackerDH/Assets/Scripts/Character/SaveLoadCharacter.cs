using System;

[Serializable]
public class SaveLoadCharacter
{
    public string name, showingName, implants, equipments, features, skills, talents, psypowers, amountsEquipments, lvlsOfFeatures, lvlsOfSkills;
    public int wounds;
    public int weaponSkill, ballisticSkill, strength, toughness, agility, intelligence, perception, willpower, fellowship, influence;
    public int weaponSkillSuper, ballisticSkillSuper, strengthSuper, toughnessSuper, agilitySuper, intelligenceSuper, perceptionSuper, willpowerSuper, fellowshipSuper, influenceSuper;
    public int half, full, natisk, run;
    public int armorHead, armorAblHead, armorTotalHead;
    public int armorRightHand, armorAblRightHand, armorTotalRightHand;
    public int armorLeftHand, armorAblLeftHand, armorTotalLeftHand;
    public int armorBody, armorAblBody, armorTotalBody;
    public int armorRightLeg, armorAblRightLeg, armorTotalRightLeg;
    public int armorLeftLeg, armorAblLeftLeg, armorTotalLeftLeg;
    public bool isTurned;
    public int shelterHead, shelterRightHand, shelterLeftHand, shelterBody, shelterRightLeg, shelterLeftLeg, shelterPoint;
}
