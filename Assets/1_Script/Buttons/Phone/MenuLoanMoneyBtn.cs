public class MenuLoanMoneyBtn : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("30000000원 남았어 분발하라고");

        base.Content();
    }
}
