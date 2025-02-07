using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class T_Settingshandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button muteButton;
    [SerializeField] private TMP_Text muteButtonText;

    private bool _isMuted;

    private void Start()
    {
        _isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateMuteButtonText();
    }

    // Toggle mute
    public void ToggleMute()
    {
        _isMuted = !_isMuted;
        PlayerPrefs.SetInt("Muted", _isMuted ? 1 : 0);
        UpdateMuteButtonText();
        Debug.Log("Muted: " + _isMuted);
    }

    // Update the mute button text
    private void UpdateMuteButtonText()
    {
        muteButtonText.text = _isMuted ? "Unmute" : "Mute";
    }
}