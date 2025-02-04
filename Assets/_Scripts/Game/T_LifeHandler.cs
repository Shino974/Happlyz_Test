using UnityEngine;
using UnityEngine.UI;

public class T_LifeHandler : MonoBehaviour
{
    public Image[] lifeImages; // Add references to the life images
    private int _lives = 3; // Initialize the number of lives

    public int Lives => _lives; // Public property to access _lives

    public void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            if (lifeImages[_lives] != null)
            {
                lifeImages[_lives].enabled = false; // Disable the life image
            }
        }
    }
}