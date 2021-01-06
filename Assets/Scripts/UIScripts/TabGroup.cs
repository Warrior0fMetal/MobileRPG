using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;
    public List<GameObject> tabPages;
    
   

    public void Subscribe(TabButton button)
    {

        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    } 

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.GetComponent<Image>().color = new Color(255, 0, 0, 255);
        int index = button.transform.GetSiblingIndex();
        
        for (int i=0; i < tabPages.Count; i++)
        {
           if (i == index)
            {
                tabPages[i].SetActive(true);
            } else
            
            {
                tabPages[i].SetActive(false);
            }

        }


    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }

    }

 
}
