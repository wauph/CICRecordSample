using System;
using System.ComponentModel;
using System.IO;
using System.Net;

using System.Collections.Generic;

using ININ.IceLib.Connection;
using ININ.IceLib.QualityManagement;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecordingSolution
{

    public class Recorder
    {
        private readonly Session _Session;
        private readonly QualityManagementManager _QualityManagementManager;
        private readonly RecordingsManager _RecordingsManager;
        private readonly ScreenRecorder _ScreenRecorder;

        private IEnumerable<Guid> m_activeRecordings;

        public Recorder(Session session)
        {
            _Session = session;
            //Create a QualityManagementManager using the connected session.
            _QualityManagementManager =  QualityManagementManager.GetInstance(_Session);
            _RecordingsManager = _QualityManagementManager.RecordingsManager;
            _ScreenRecorder = new ScreenRecorder(_QualityManagementManager);
        }

        public async Task<HttpResponseMessage> GetRecording(string recordingId, string participantId)
        {
            var recordingUri = _RecordingsManager.GetExportUri(recordingId, RecordingMediaType.PrimaryMedia, participantId, 0);
            return await ConvertWavFile(recordingUri);
        }

        private async Task<HttpResponseMessage> ConvertWavFile(Uri uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(uri);
            }
        }
    }
}
