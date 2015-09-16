using System.ServiceProcess;

namespace ephoneTables
{

    partial class EphoneService : ServiceBase
    {

         // na początku będzie puste
        private EphoneMain main = null;

        public EphoneService()
        {

            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            main = new EphoneMain();
        }

        protected override void OnStop()
        {
            EventLogging.LogEvent("Service Aborted", true);
            main.Abort();
        }
    }
}
