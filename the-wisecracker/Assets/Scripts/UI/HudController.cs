using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    private WeaponController weaponController;
    private PlayerController playerController;

    public Text clipAmmo;
    public Text stockAmmo;
    public Text separator;
    public Text weapon;
    public Image swapIndicator;

    public Slider hp;
    public Slider reloadIndicator;

    public GameObject hurtIndicatorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        weaponController = Utils.FindGameObject(Utils.Tags.PLAYER)
            .GetComponent<WeaponController>();
        playerController = Utils.FindGameObject(Utils.Tags.PLAYER)
            .GetComponent<PlayerController>();

        playerController.HurtEvent += OnPlayerHurt;

        separator.text = "|";
        reloadIndicator.gameObject.SetActive(false);
    }

    private void OnPlayerHurt()
    {
        Instantiate(hurtIndicatorPrefab, transform)
            .SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        clipAmmo.text = weaponController
            .Current
            .ClipAmmo;
        clipAmmo.color = ColorFromAmmo(weaponController.CurrentWeaponClipState);

        stockAmmo.text = weaponController
            .Current
            .StockAmmo;
        stockAmmo.color = ColorFromAmmo(weaponController.CurrentWeaponStockState);

        weapon.text = weaponController.Current.Type;

        reloadIndicator.gameObject.SetActive(weaponController.IsReloading);
        if (weaponController.IsReloading)
            reloadIndicator.value = weaponController.PercentageReloaded;

        swapIndicator.enabled = weaponController.IsSwaping;
        hp.value = playerController.Hp;
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
