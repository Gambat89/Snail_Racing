public class MenuMoneyBtn : MenuBtn
{
    protected override void Content()
    {
        menu.SetActive(false);
        content.SetActive(true);

        contentText.text = string.Format("���� �����Ͻ� �ݾ���\n{0}$ �Դϴ�.", GambleManager.instance.playerMoney);

        base.Content();
    }
}