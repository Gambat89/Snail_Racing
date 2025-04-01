public class MenuBrokerButton : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("무슨 일이신가요?");

        base.Content();
    }
}
