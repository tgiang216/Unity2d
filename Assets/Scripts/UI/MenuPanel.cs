using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        AudioManager.Instance.PlayBGM("BGM_Title");
    }

    public void OnClickedStartGameButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveLoadingPanel(true);
            UIManager.Instance.ActiveMenuPanel(false);
        }
    }

    public void OnSettingButtonClick()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveSettingPanel(true);
        }
    }
    private IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(2f);
        
    }
}
