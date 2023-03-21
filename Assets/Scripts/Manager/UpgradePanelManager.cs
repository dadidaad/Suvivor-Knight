using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] GameObject notificationPanel;
    private void Awake()
    {
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Notification notification = notificationPanel.GetComponent<Notification>();
        StartCoroutine(notification.ShowMessage("LEVEL UP!!", 1f));
        StartCoroutine(ActivePanel(upgradeDatas));
    }

    IEnumerator ActivePanel(List<UpgradeData> upgradeDatas)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        panel.SetActive(true);
        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }
    public void ClosePanel()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void Upgrade(int pressedButtonId)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Level>().Upgrade(pressedButtonId);
        ClosePanel();
    }
}
