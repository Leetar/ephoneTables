using System;

namespace ephoneTables
{
    class TerminateProgram
    {
        /// <summary>
        /// Exits program after showing error message
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        public TerminateProgram(string errorMessage)
        {
            Console.WriteLine(errorMessage + Environment.NewLine + "Terminating program due to error occurence...");
            System.Threading.Thread.Sleep(5000);
            Environment.Exit(0);
        }

        public TerminateProgram(Exception ex)
        {
            Console.WriteLine(ex.Message + Environment.NewLine + "Terminating program due to error occurence...");
            System.Threading.Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}
