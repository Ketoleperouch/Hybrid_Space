using UnityEngine;
using UnityEngine.UI;

public class StateBehaviour : MonoBehaviour {

    public static StateBehaviour me;

    public Image overlayImage;
    public Sprite overlayTexture;

    public int progress = 0;

    private void Start()
    {
        if (!me)
            me = this;
        else
            Destroy(this);
    }

    public void ChangeSprite(Sprite to)
    {
        overlayImage.sprite = to;
    }

    public void SetProgress(int prog)
    {
        progress = prog;
    }

    private void OnStateChange()
    {
        overlayImage.sprite = overlayTexture;
    }
}
