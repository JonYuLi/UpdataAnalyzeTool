using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IapServer
{
    public class IapServerProc
    {
        public string m_pFilename;
        int m_nFileLength = 0;
        int m_nVerify = 0;
        int m_nSumVerify;
        int m_nCrcSumVerify;
        int m_nPackageNum;
        byte[] m_strNewVersion = new byte[2];
        int m_nFileType;
        byte[] m_Rev = new byte[]{ 0, 0, 0, 0};
        private readonly uint SOFTWARE_PACKAGE_SIZE = 512;

        public IapServerProc(string filename)
        {
            m_pFilename = filename;
            GetFileInfo();
        }

        public bool MakeSSWMessage(byte[] buffer)
        {
            buffer[0] = (byte)'S';
            buffer[1] = (byte)'S';
            buffer[2] = (byte)'W';
            buffer[3] = (byte)':';
            buffer[4] = (byte)(m_nFileType & 0xFF);
            buffer[5] = (byte)((m_nFileLength & 0xFF00) >> 8);
            buffer[6] = (byte)(m_nFileLength & 0x00FF);
            buffer[7] = (byte)((m_nVerify & 0xFF00) >> 8);
            buffer[8] = (byte)(m_nVerify & 0x00FF);
            buffer[9] = (byte)((m_nSumVerify & 0xFF00) >> 8);
            buffer[10] = (byte)(m_nSumVerify & 0x00FF);

            buffer[11] = (byte)((m_nCrcSumVerify & 0xFF000000) >> 24);
            buffer[12] = (byte)((m_nCrcSumVerify & 0x00FF0000) >> 16);
            buffer[13] = (byte)((m_nCrcSumVerify & 0x0000FF00) >> 8);
            buffer[14] = (byte)(m_nCrcSumVerify & 0x00FF);

            buffer[15] = (byte)((m_nPackageNum & 0xFF00) >> 8);
            buffer[16] = (byte)(m_nPackageNum & 0x00FF);
            buffer[17] = m_strNewVersion[0];
            buffer[18] = m_strNewVersion[1];
            buffer[19] = m_Rev[0];
            buffer[20] = m_Rev[1];
            byte verify = 0;
            for (int i = 4; i < 21; i++)
                verify ^= buffer[i];
            buffer[21] = verify;
            buffer[22] = (byte)'E';
            buffer[23] = (byte)'N';
            buffer[24] = (byte)'D';
            return true;
        }

        public bool MakeUSWMessage(byte[] buffer, int package)
        {
            return false;
        }

        public bool GetFileInfo()
        {
            UInt32 nDesFileLength = 0;
            byte[] buff = new byte[10];

            if (m_pFilename == null) return false;
            try
            {
                FileStream fs = File.OpenRead(m_pFilename);
                if (fs == null) return false;

                m_nFileType = 0;

                if (fs.Read(buff, 0, 2) != 2) return false;
                m_nFileLength = buff[0] | buff[1] << 8;
                if (fs.Read(buff, 0, 2) != 2) return false;
                m_nVerify = buff[0] | buff[1] << 8;
                if (fs.Read(buff, 0, 2) != 2) return false;
                m_nSumVerify = buff[0] | buff[1] << 8;
                if (fs.Read(buff, 0, 4) != 4) return false;
                m_nCrcSumVerify = buff[3] << 24 | buff[2] << 16 | buff[1] << 8 | buff[0];
                if (fs.Read(buff, 0, 2) != 2) return false;
                m_strNewVersion[0] = buff[0];
                m_strNewVersion[1] = buff[1];

                nDesFileLength = (uint)(m_nFileLength + (m_nFileLength % 16 > 0 ? 16 - m_nFileLength % 16 : 0));
                m_nPackageNum = (int)(nDesFileLength % SOFTWARE_PACKAGE_SIZE > 0 ? nDesFileLength / SOFTWARE_PACKAGE_SIZE + 1 : nDesFileLength / SOFTWARE_PACKAGE_SIZE);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("IapServer读文件信息时出错：" + ex.Message);
                return false;
            }
        }
    }
}
