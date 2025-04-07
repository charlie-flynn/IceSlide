using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
