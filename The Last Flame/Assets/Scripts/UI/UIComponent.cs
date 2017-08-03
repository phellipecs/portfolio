using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour {

    public Toggle[] resolutionsToggle;
    public int[] screenWidths;

    public void ToggleFullScreen()
    {
        GameManager.instance.ToggleFullScreen();
    }

    public void PauseGame()
    {
        GameManager.instance.TogglePause();
    }

    public void LoadScene(string scene)
    {
        LevelManager.instance.LoadScene(scene);
    }

    public void UparSkill(int skillN)
    {
        ManagerXp.instance.GastarPontos(skillN);
    }

    public void ScreenResolution(int i)
    {
        if (resolutionsToggle[i].isOn)
        {
            float aspectRatio=19/9f;
            Screen.SetResolution(screenWidths[i],(int)(screenWidths[i] / aspectRatio), false);
        }
    }

    public void SairDoJogo()
    {
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); 
        Application.Quit();
        //Application.LoadLevel(0);
    }

}
