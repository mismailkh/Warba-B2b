namespace WB.Web.Services
{
    public class SpinnerService
    {
        public event Action OnShow;
        public event Action OnNewShow;
        public event Action OnSigningShow;
        public event Action OnHide;

        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }
    }
}
