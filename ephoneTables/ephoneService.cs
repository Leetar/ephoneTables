using System.ServiceProcess;

namespace ephoneTables
{

    partial class EphoneService : ServiceBase
    {

         // na początku będzie puste

        
        public EphoneService()
        {
            
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            EphoneMain epmain = new EphoneMain();
        }

        protected override void OnStop()
        {
            EphoneMain epmain = new EphoneMain();
            epmain.Abort();
        }
    }
}
