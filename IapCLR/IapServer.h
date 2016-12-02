#pragma once

#define SOFTWARE_PACKAGE_SIZE	512
#include "stdafx.h"

typedef struct
{
	int  nUpdateSoftwareStatus;
	bool bPackageUpdateFlag[128];
	char sCurrentSoftwareVersion[3];
	char sNewSoftwareVersion[3];
	int  nUpdateResult;
} tQSWResponDef;

class CIapServer
{
public:
	CIapServer(char *filename);
	~CIapServer(void);
	bool MakeSSWMessage(unsigned char* buffer, int max_len, int* buffer_len);
	bool GetSSWResp(unsigned char *buffer, int buffer_len, int *respCode);
	bool MakeUSWMessage(unsigned char* buffer, int max_len, int* buffer_len, int nPackage);
	bool GetUSWResp(unsigned char *buffer, int buffer_len, int *nPackage, int *respCode);
	bool MakeQSWMessage(unsigned char* buffer, int max_len, int* buffer_len);
	bool GetQSWResp(unsigned char *buffer, int buffer_len, tQSWResponDef *resp);
	int  GetPackageNum();
private:
	bool GetFileInfo();
private:
	char *m_pFilename;
	int m_nFileLength;
	int m_nVerify;
	int m_nSumVerify;
	int m_nCrcSumVerify;
	int m_nPackageNum;
	char m_strNewVersion[2];
	int m_nFileType;
	unsigned char m_Rev[4];
	FILE *m_fp;
};
