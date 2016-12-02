// IapCLR.h

#pragma once
#include "IapServer.h"

using namespace System;

namespace IapCLR {

	public ref class Iap
	{
		char * filePathName;
		CIapServer * iapServer;
		// TODO:  在此处添加此类的方法。
	public:
		Iap(String ^ file)
		{
			filePathName = (char*)(void*)System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(file);
			iapServer = new CIapServer(filePathName);
		}

		array<Byte> ^ GetSSWPackage();
		array<Byte> ^ GetUSWPackage(int packageNum);
		array<Byte> ^ GetQSWPackage();
		int GetPackageNum();
	};
}
