public class MenuLoanMoneyBtn : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("30000000�� ���Ҿ� �й��϶��");

        base.Content();
    }
}
