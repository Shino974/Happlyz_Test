using UnityEngine;
using UnityEngine.UI;

public class T_LifeHandler : MonoBehaviour
{
    public Image[] lifeImages;
    public int lives = 3;
    
    // Lose life function
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            if (lifeImages[lives] != null)
                lifeImages[lives].enabled = false;
        }
    }
}