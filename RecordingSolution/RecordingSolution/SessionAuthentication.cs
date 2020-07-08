using ININ.IceLib.Connection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingSolution
{
    public class SessionAuthentication
    {
        private readonly Session _Session;
        public SessionAuthentication(Session session)
        {
            _Session = session;
        }

        public Session ConnectToCICServer(SessionModel sessionModel)
        {
            _Session.Connect(
                    new SessionSettings(),
                    GetHostSettings(
                        sessionModel.ServerHost,
                        sessionModel.serverPort
                        ),
                    GetAuthSettings(
                        sessionModel.userName,
                        sessionModel.userPassword
                        ),
                    new StationlessSettings()
                    );
            return _Session;
        }

        private HostSettings GetHostSettings(string serverHost, int serverPort)
        {
            HostSettings hostSettings = new HostSettings();
            hostSettings.HostEndpoint = new HostEndpoint
                                (
                                 serverHost,
                                 serverPort
                                );

            return hostSettings;
        }

        private AuthSettings GetAuthSettings(string userName, string password)
        {
            AuthSettings authSettings;

            authSettings = new ICAuthSettings(
                userName, password
                );
            return authSettings;
        }
    }
}
