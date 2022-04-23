using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnimateOnHover : MonoBehaviour
{
    private Vector3 initialLocalScale;

    private bool isLoadingRunning = false;

    [SerializeField] GameObject LoadingCube;

    public void Start()
    {
        this.initialLocalScale = LoadingCube.transform.localScale;
        LoadingCube.SetActive(false);
    }

    public void Awake()
    {
        float duration = 0.1F;
        InvokeRepeating("HandleLoadingState", 0, duration);
    }

    public void HandleLoadingState()
    {
        if (this.isLoadingRunning)
        {
            LoadingCube.SetActive(true);
            this.LoadingCube.transform.localScale = new Vector3(this.LoadingCube.transform.localScale.x + 1, 
                                                                this.LoadingCube.transform.localScale.y, 
                                                                this.LoadingCube.transform.localScale.z
                                                     );

            if (this.LoadingCube.transform.localScale.x >= transform.localScale.x)
            {
                SceneManager.LoadScene(1); 
            }
        }
    }

    public void OnHoverEntered()
    {
        this.isLoadingRunning = true;
    }

    public void OnHoverExited()
    {
        this.isLoadingRunning = false;
        this.LoadingCube.SetActive(false);
        this.LoadingCube.transform.localScale = initialLocalScale;
    }
}
