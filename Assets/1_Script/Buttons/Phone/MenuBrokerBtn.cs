public class MenuBrokerButton : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("���� ���̽Ű���?");

        base.Content();
    }
}
