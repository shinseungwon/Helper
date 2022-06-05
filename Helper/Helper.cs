namespace Helper
{
    /// <summary>
    /// Helper Mother Class
    /// </summary>
    public class Helper
    {
        public LogHelper Logger = null;
        public Helper()
        {

        }

        public Helper(LogHelper logger)
        {
            Logger = logger;
        }

        public void SetLogger(LogHelper logger)
        {
            Logger = logger;
        }

        protected void WriteLog(string text, string name = null)
        {
            if (Logger != null)
            {
                if(name == null)
                {
                    Logger.WriteText(text);
                }
                else
                {
                    Logger.WriteFile(text, name);
                }                
            }
        }        
    }
}
