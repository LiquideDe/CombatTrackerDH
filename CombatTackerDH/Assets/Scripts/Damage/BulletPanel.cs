using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BulletPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI textName;
    public delegate void RemoveBullet(BulletPanel bulletPanel);
    RemoveBullet removeBullet;
    private Bullet bullet;
    public Bullet Bullet { get => bullet; }
    public void OnPointerDown(PointerEventData eventData)
    {
        removeBullet?.Invoke(this);
    }

    public void SetParams(RemoveBullet removeBullet, string place, string penetration, string damage, bool isIgnoreArmor, bool isIgnoreToghness, bool isWarpWeapon)
    {
        gameObject.SetActive(true);
        this.removeBullet = removeBullet;
        textName.text = damage;
        bullet = new Bullet(place, penetration, damage, isIgnoreArmor, isIgnoreToghness, isWarpWeapon);
    }
}
