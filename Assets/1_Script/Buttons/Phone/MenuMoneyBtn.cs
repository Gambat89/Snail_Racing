public class MenuMoneyBtn : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("현재 보유하신 금액은\n{0}$ 입니다.", GambleManager.instance.playerMoney);

        base.Content();
    }
}