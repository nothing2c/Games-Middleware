using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] Menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        foreach(Menu m in Menus)
        {
            if (m.MenuName == menuName)
                m.Open();
            else if (m.isOpen)
                CloseMenu(m);
        }
    }

    public void OpenMenu(Menu menu)
    {
        foreach (Menu m in Menus)
        {
            if (m.isOpen)
                CloseMenu(m);
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
