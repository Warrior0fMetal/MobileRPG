using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.Audio.AudioMixer audioMixer;
    // Start is called before the first frame update
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("QUIT!!");
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qulityIndex)
    {
        QualitySettings.SetQualityLevel(qulityIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ContinueGame()
    {

    }


}
