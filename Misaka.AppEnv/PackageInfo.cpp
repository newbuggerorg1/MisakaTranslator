#include "Percompilation.h"
#include "PackageInfo.h"
#include "PackageInfo.g.cpp"

namespace winrt::Misaka::AppEnv::implementation
{
    bool PackageInfo::RunWithId()
    {
        static const bool cache = []()->bool
        {
            UINT32 bufferLength = 0;
            LONG f = GetCurrentPackageId(&bufferLength, nullptr);
            switch (f)
            {
            case ERROR_SUCCESS:
            case ERROR_INSUFFICIENT_BUFFER:
                return true;
            case APPMODEL_ERROR_NO_PACKAGE:
            default:
                return false;
            }
        }();
        return cache;
    }

    hstring PackageInfo::InstalledFolder()
    {
        static const hstring cache = []()->hstring
        {
            if (RunWithId())
            {
                return Windows::ApplicationModel::Package::Current().InstalledLocation().Path();
            }
            else
            {
                std::filesystem::path appPath = wil::GetModuleFileNameW<wil::unique_cotaskmem_string>(wil::GetModuleInstanceHandle()).get();
                return appPath.parent_path().c_str();
            }
        }();
        return cache;
    }

    hstring PackageInfo::LocalFolder()
    {
        static const hstring cache = []()->hstring
        {
            if (RunWithId())
            {
                return Windows::Storage::AppDataPaths::GetDefault().LocalAppData();
            }
            else
            {
                return std::format(L"{}\\Data", InstalledFolder()).c_str();
            }
        }();
        return cache;
    }

    hstring PackageInfo::TemporaryFolder()
    {
        static const hstring cache = []()->hstring
        {
            if (RunWithId())
            {
                return Windows::Storage::ApplicationData::Current().TemporaryFolder().Path();
            }
            else
            {
                return wil::GetEnvironmentVariableW<wil::unique_cotaskmem_string>(L"TEMP").get();
            }
        }();
        return cache;
    }
}
