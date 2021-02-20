using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    private WeaponStats stats;

    public Text clipAmmo;
    public Text stockAmmo;
    public Text separator;
    public Text weapon;
    public Image swapIndicator;

    public Slider reloadIndicator;

    private Vector3 reloadTimeOffset = Vector3.up * -3f;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject
            .FindGameObjectWithTag("Database")
            .GetComponent<WeaponStats>();

        separator.text = "|";
        reloadIndicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        clipAmmo.text = stats
            .Current
            .ClipAmmo;
        clipAmmo.color = ColorFromAmmo(stats.CurrentWeaponClipState);

        stockAmmo.text = stats
            .Current
            .StockAmmo;
        stockAmmo.color = ColorFromAmmo(stats.CurrentWeaponStockState);

        weapon.text = stats.Current.Type;

        reloadIndicator.gameObject.SetActive(stats.IsReloading);
        if (stats.IsReloading)
            reloadIndicator.value = stats.PercentageReloaded;

        swapIndicator.enabled = stats.IsSwaping;
    }

    private Color ColorFromAmmo(AmmoState ammoState)
    {
        return ammoState == AmmoState.STANDARD
                ? Color.white
                : ammoState == AmmoState.CONCERNING
                    ? Color.yellow
                    : Color.red;
    }
}
