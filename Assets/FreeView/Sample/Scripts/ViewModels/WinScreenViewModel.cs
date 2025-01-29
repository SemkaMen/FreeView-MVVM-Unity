using System;
using FreeView.ViewModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FreeView.Sample.Scripts.ViewModels
{
    public class WinScreenViewModel : BaseViewModel
    {
        public void Reset()
        {
            try
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}