#include "IapServer.h"

CIapServer::CIapServer(char *filename)
{
	m_pFilename = filename;
	m_nFileLength = 0;
	m_nVerify = 0;
	m_fp = NULL;
	memset(m_Rev, 0, 4);
	GetFileInfo();
}

CIapServer::~CIapServer(void)
{
	if(m_fp)	fclose(m_fp);
}

bool CIapServer::GetFileInfo()
{
	unsigned int nRead = 0, nCount = 0, nDesFileLength = 0;

	if(!m_pFilename)	return false;
	fopen_s(&m_fp, m_pFilename, "rb");
	if(!m_fp)			return false;

	m_nFileType = 0;
	if(fread(&m_nFileLength, sizeof(unsigned char), 2, m_fp) != 2)	return false;
	if(fread(&m_nVerify, sizeof(unsigned char), 2, m_fp) != 2)	return false;
	if(fread(&m_nSumVerify, sizeof(unsigned char), 2, m_fp) != 2)	return false;
	if(fread(&m_nCrcSumVerify, sizeof(unsigned char), 4, m_fp) != 4)	return false;

	if(fread(&m_strNewVersion[0], sizeof(unsigned char), 1, m_fp) != 1)	return false;
	if(fread(&m_strNewVersion[1], sizeof(unsigned char), 1, m_fp) != 1)	return false;
	
	nDesFileLength = m_nFileLength + (m_nFileLength % 16 ? 16 - m_nFileLength % 16 : 0);
	m_nPackageNum = nDesFileLength % SOFTWARE_PACKAGE_SIZE ? nDesFileLength / SOFTWARE_PACKAGE_SIZE + 1 : nDesFileLength / SOFTWARE_PACKAGE_SIZE;
	return true;
}

bool CIapServer::MakeSSWMessage(unsigned char* buffer, int max_len, int* buffer_len)
{
	unsigned char verify = 0;
	if(!buffer)			return false;
	if(max_len < 21)	return false;

	buffer[0] = 'S';
	buffer[1] = 'S';
	buffer[2] = 'W';
	buffer[3] = ':';
	buffer[4] = m_nFileType & 0xFF;
	buffer[5] = (m_nFileLength & 0xFF00) >> 8;
	buffer[6] = m_nFileLength & 0x00FF;
	buffer[7] = (m_nVerify & 0xFF00) >> 8;
	buffer[8] = m_nVerify & 0x00FF;
	buffer[9] = (m_nSumVerify & 0xFF00) >> 8;
	buffer[10] = m_nSumVerify & 0x00FF;

	buffer[11] = (m_nCrcSumVerify & 0xFF000000) >> 24;
	buffer[12] = (m_nCrcSumVerify & 0x00FF0000) >> 16;
	buffer[13] = (m_nCrcSumVerify & 0x0000FF00) >> 8;
	buffer[14] = m_nCrcSumVerify & 0x00FF;

	buffer[15] = (m_nPackageNum & 0xFF00) >> 8;
	buffer[16] = m_nPackageNum & 0x00FF;
	buffer[17] = m_strNewVersion[0];
	buffer[18] = m_strNewVersion[1];
	buffer[19] = m_Rev[0];
	buffer[20] = m_Rev[1];
	for(int i=4; i<21; i++)
		verify ^= buffer[i];
	buffer[21] = verify;
	buffer[22] = 'E';
	buffer[23] = 'N';
	buffer[24] = 'D';
	*buffer_len = 25;
	return true;
}

bool CIapServer::GetSSWResp(unsigned char *buffer, int buffer_len, int *respCode)
{
	if(!buffer)			return false;
	if(buffer_len < 11)	return false;
	char *pos = strstr((char*)buffer, "SSW:");
	if(!pos)			return false;
	int begin = pos - (char*)buffer;
	if(memcmp(&buffer[begin+8], "END", 3) != 0)	return false;
	
	if(buffer[begin+7] != (buffer[begin+4] ^ buffer[begin+5] ^ buffer[begin+6]))	return false;
	*respCode = buffer[begin+4];
	return true;
}

bool CIapServer::MakeUSWMessage(unsigned char* buffer, int max_len, int* buffer_len, int nPackage)
{
	unsigned char verify = 0;

	if(!m_fp)			return false;
	if(!buffer)			return false;
	if(max_len < 255)	return false;
	if(nPackage >= m_nPackageNum)	return false;

	fseek(m_fp, 12+nPackage*SOFTWARE_PACKAGE_SIZE, SEEK_SET);
	
	buffer[0] = 'U';
	buffer[1] = 'S';
	buffer[2] = 'W';
	buffer[3] = ':';
	buffer[4] = nPackage;

	int len = fread(&buffer[7], sizeof(unsigned char), SOFTWARE_PACKAGE_SIZE, m_fp);
	buffer[5] = (len & 0xFF00 ) >> 8;
	buffer[6] = len & 0xFF;
	for(int i=len; i<SOFTWARE_PACKAGE_SIZE; i++)
		buffer[7+i] = 0xFF;

	for(int i=4; i<519; i++)
		verify ^= buffer[i];
	buffer[519] = verify;
	buffer[520] = 'E';
	buffer[521] = 'N';
	buffer[522] = 'D';
	*buffer_len = 523;
	return true;
}

bool CIapServer::GetUSWResp(unsigned char *buffer, int buffer_len, int *nPackage, int *respCode)
{
	if(!buffer)			return false;
	if(buffer_len < 10)	return false;

	int i;
	for(i=0; i<buffer_len-4; i++)
	{
		if(buffer[i] == 'U' && buffer[i+1] == 'S' && buffer[i+2] == 'W' && buffer[i+3] == ':')
		{
			break;
		}
	}
	if(i >= buffer_len-4)					return false;
	if(memcmp(&buffer[i+7], "END", 3) != 0)	return false;

	if(buffer[i+6] != (buffer[i+4] ^ buffer[i+5]))	return false;
	*nPackage = buffer[i+4];
	*respCode = buffer[i+5];
	return true;
}

bool CIapServer::MakeQSWMessage(unsigned char* buffer, int max_len, int* buffer_len)
{
	unsigned char verify = 0;
	if(!buffer)			return false;
	if(max_len < 9)	return false;

	buffer[0] = 'Q';
	buffer[1] = 'S';
	buffer[2] = 'W';
	buffer[3] = ':';
	buffer[4] = 0x00;
	buffer[5] = 'E';
	buffer[6] = 'N';
	buffer[7] = 'D';
	*buffer_len = 8;
	return true;
}

bool CIapServer::GetQSWResp(unsigned char *buffer, int buffer_len, tQSWResponDef *resp)
{
	if(!buffer)			return false;
	if(buffer_len < 12)	return false;
	char *pos = strstr((char*)buffer, "QSW:");
	if(!pos)			return false;
	int begin = pos - (char*)buffer;
	if(memcmp(&buffer[begin+27], "END", 3) != 0)		return false;

	int verify = 0;
	for(int i=4; i<26; i++)
		verify ^= buffer[begin+i];

	if(buffer[begin+26] != verify)	return false;

	resp->nUpdateSoftwareStatus = buffer[begin+4];
	for(int i=5; i<21; i++)
	{
		for(int j=0; j<8; j++)
		{
			if((buffer[begin+i] & (0x80 >> j)) != 0)
				resp->bPackageUpdateFlag[(i-5)*8+j] = true;
			else
				resp->bPackageUpdateFlag[(i-5)*8+j] = false;
		}
	}
	memcpy(resp->sCurrentSoftwareVersion, &buffer[begin+21], 2);
	resp->sCurrentSoftwareVersion[2] = 0;

	memcpy(resp->sNewSoftwareVersion, &buffer[begin+23], 2);
	resp->sNewSoftwareVersion[2] = 0;

	resp->nUpdateResult = buffer[begin+25];

	return true;
}

int  CIapServer::GetPackageNum()
{
	return m_nPackageNum;
}