using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagePanel : MonoBehaviour
{
    [SerializeField] BulletPanel bulletExample;
    [SerializeField] Transform content;
    [SerializeField] TMP_InputField inputPenetration, inputDamage, inputPlace;
    [SerializeField] Toggle toggleIgnoreArmor, toggleIgnoreToughness, toggleWarpWeapon;
    public delegate void ReturnBulets(List<Bullet> bullets);
    ReturnBulets returnBulets;
    List<BulletPanel> bulletPanels = new List<BulletPanel>();
    public void SetParams(ReturnBulets returnBulets)
    {
        gameObject.SetActive(true);
        this.returnBulets = returnBulets;
    }

    public void AddDamage()
    {
        if(inputDamage.text.Length > 0 && inputPlace.text.Length > 0)
        {
            bulletPanels.Add(Instantiate(bulletExample, content));
            bulletPanels[^1].SetParams(RemoveBullet, inputPlace.text, inputPenetration.text, inputDamage.text, toggleIgnoreArmor.isOn, toggleIgnoreToughness.isOn, toggleWarpWeapon.isOn);
        }
        
    }

    private void RemoveBullet(BulletPanel bulletPanel)
    {
        foreach(BulletPanel bullet in bulletPanels)
        {
            if(bullet == bulletPanel)
            {
                bulletPanels.Remove(bullet);
                Destroy(bullet.gameObject);
                break;
            }
        }
    }

    public void Done()
    {
        Debug.Log($"Нажали");
        List<Bullet> bullets = new List<Bullet>();
        foreach(BulletPanel bullet in bulletPanels)
        {
            bullets.Add(bullet.Bullet);
            Debug.Log($"Добавили пулю {bullet.Bullet.Damage}");
        }
        returnBulets?.Invoke(bullets);
        Cancel();
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
