// 这是主 DLL 文件。

#include "stdafx.h"

#include "IapCLR.h"

using namespace IapCLR;
using namespace System::Runtime::InteropServices;

array<Byte> ^ Iap::GetSSWPackage()
{
	unsigned char sswData[50];
	int len = 0;
	if (iapServer->MakeSSWMessage(sswData, 50, &len))
	{
		//return sswData;
		array< Byte >^ byteArray = gcnew array< Byte >(len);
		Marshal::Copy((IntPtr)sswData, byteArray, 0, len);
		return byteArray;
	}
	array< Byte >^ byteArray = gcnew array< Byte >(0);
	return byteArray;
}

array<Byte> ^ Iap::GetUSWPackage(int packageNum)
{
	unsigned char sswData[550];
	int len = 0;
	if (iapServer->MakeUSWMessage(sswData, 550, &len, packageNum))
	{
		//return sswData;
		array< Byte >^ byteArray = gcnew array< Byte >(len);
		Marshal::Copy((IntPtr)sswData, byteArray, 0, len);
		return byteArray;
	}
	array< Byte >^ byteArray = gcnew array< Byte >(0);
	return byteArray;
}

array<Byte> ^ Iap::GetQSWPackage()
{
	unsigned char sswData[50];
	int len = 0;
	if (iapServer->MakeQSWMessage(sswData, 50, &len))
	{
		//return sswData;
		array< Byte >^ byteArray = gcnew array< Byte >(len);
		Marshal::Copy((IntPtr)sswData, byteArray, 0, len);
		return byteArray;
	}
	array< Byte >^ byteArray = gcnew array< Byte >(0);
	return byteArray;
}

int Iap::GetPackageNum()
{
	return iapServer->GetPackageNum();
}