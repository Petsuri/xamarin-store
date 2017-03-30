using Store.Interface.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service
{
    public class InternetAccessService
    {

        private readonly IApplication m_application;
        private readonly IInternetConnection m_internet;

        public InternetAccessService(IInternetConnection internet, IApplication application)
        {
            m_internet = internet;
            m_application = application;
        }

        public async Task RequestInternetAccess()
        {            
            if (!m_internet.IsConnected())
            {
                while (!m_internet.IsConnected())
                {
                    bool isCanceled = !(await m_internet.RequestConnection());
                    if (isCanceled)
                    {
                        m_application.Close();
                    }
                }
            }
        }

    }
}
