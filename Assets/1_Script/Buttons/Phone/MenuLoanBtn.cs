public class MenuLoanBtn : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("�� ��������?");

        base.Content();
    }
}
