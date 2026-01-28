using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private MonoBehaviour progressSource;
    [SerializeField] private Image barImage;

    private void Start() {
        barImage.fillAmount = 0f;

        if (progressSource is not IHasProgress) {
            Debug.LogError("progressSource does not implement IHasProgress");
        }
        (progressSource as IHasProgress).OnProgressChanged += HandleProgressChanged;

        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void HandleProgressChanged(float progressNormalized) {
        barImage.fillAmount = progressNormalized;
        if (progressNormalized == 0f || progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }
}
