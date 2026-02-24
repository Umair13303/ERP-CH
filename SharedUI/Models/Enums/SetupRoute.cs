namespace SharedUI.Models.Enums
{
    public class SetupRoute
    {
        public enum Area
        {
            ApplicationConfiguration=1,
            AccountNfinance=2,
            AIMSCHConfiguration = 3,
        }
        public enum Controller
        {
            COMDashboard = 1,
            COMAuthentication = 2,
        }
        public enum Action
        {
            Login = 1,
            DashboardDefault = 1,
        }
        public enum Api
        {
            COMInternalAPI=1
        }
    }
}
