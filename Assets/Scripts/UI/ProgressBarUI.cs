using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ProgressBarUI : MonoBehaviour {

    [SerializeField] private GameObject hasProgressObject;
    [SerializeField] private Image barImage;

    private IHasProgress _hasProgressObject;
    private void Start() {
        _hasProgressObject = hasProgressObject.GetComponent<IHasProgress>();
        if (_hasProgressObject == null) {
            Debug.LogError("ProgressBarUI: No IHasProgress component attached");
        }
        
        _hasProgressObject.OnProgressChanged += HasProgressObjectOnOnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgressObjectOnOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
