using StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class Core : MonoBehaviour
    {
        private void Awake()
        {
            if (SceneManager.GetActiveScene().name != SceneNames.BOOT)
            {
                Debug.LogWarning("You didn't run the game from the " + SceneNames.BOOT +
                                 " scene, it's undesirable case because it may lead to a violation of the initialization logic " +
                                 "(in the future, when the project has millions of entities on game scene)");
                return;
            }

            SceneManager.LoadScene(SceneNames.GAME);
        }
    }
}
