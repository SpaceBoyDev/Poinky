using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;
using Image = UnityEngine.UI.Image;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private AsyncOperation sceneLoading;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image panelImage;
    [SerializeField] private float lerpSpeed = 1f;

    [SerializeField] bool loading = false;
    [SerializeField] private bool isLoadAnimationDone = false;

    [SerializeField] private Color panelColor;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        panelImage = panel.GetComponent<Image>();

        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);

        canvas.SetActive(false);
        panel.SetActive(false);
        
    }

    public void Load(int sceneToLoad, bool autoAnimation = true)
    {
        canvas.SetActive(true);
        panel.SetActive(true);
        

        if (autoAnimation)
        {
            StartCoroutine(LoadWithAnimation(sceneToLoad));
        }
        else
        {
            StartCoroutine(OnlyLoad(sceneToLoad));
        }
    }
    
    private IEnumerator OnlyLoad(int sceneToLoad)
    {
        //PlayerInputManager.Instance.IsInputAllowed = false;
        //PlayerInputManager.Instance.IsPauseAllowed = false;

        sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);
        loading = true;
        
        while (loading)
        {
            if (sceneLoading.progress == 1)
            {
                loading = false;
            }

            yield return null;
        }

        yield return null;

    }
    
    private IEnumerator LoadWithAnimation(int sceneToLoad)
    {
        //PlayerInputManager.Instance.IsInputAllowed = false;
        //PlayerInputManager.Instance.IsPauseAllowed = false;

        panelImage.DOColor(new Color(panelColor.r, panelColor.g, panelColor.b, 1f), 0.2f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1f);
        
        sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);
        loading = true;
        
        while (loading)
        {
            if (sceneLoading.progress == 1)
            {
                loading = false;
                yield return new WaitForSecondsRealtime(0.2f);
                
                panelImage.DOColor(new Color(panelColor.r, panelColor.g, panelColor.b, 0f), 0.6f).SetUpdate(true);;
            }

            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.6f);
        
        panel.SetActive(false);
        canvas.SetActive(false);
        
    }

    public void LoadingAnimationStart()
    {
        StartCoroutine(LoadingAnimationStartCoroutine());
    }
    
    private IEnumerator LoadingAnimationStartCoroutine()
    {
        panelImage.DOColor(new Color(panelColor.r, panelColor.g, panelColor.b, 1f), 0.2f).SetUpdate(true);

        yield return null;
    }
    
    public void LoadingAnimationFinish()
    {
        StartCoroutine(LoadingAnimationFinishCoroutine());
    }
    
    private IEnumerator LoadingAnimationFinishCoroutine()
    {
        panelImage.DOColor(new Color(panelColor.r, panelColor.g, panelColor.b, 0f), 0.6f).SetUpdate(true);;
        
        yield return new WaitForSecondsRealtime(0.6f);
        
        panel.SetActive(false);
        canvas.SetActive(false);
    }
}
