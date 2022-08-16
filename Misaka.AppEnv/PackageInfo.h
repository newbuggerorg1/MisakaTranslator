#pragma once

#include "PackageInfo.g.h"

namespace winrt::Misaka::AppEnv::implementation
{
    static class PackageInfo : public PackageInfoT<PackageInfo>
    {
    public:
        PackageInfo() = delete;
        static bool RunWithId();
        static hstring InstalledFolder();
        static hstring LocalFolder();
        static hstring TemporaryFolder();
    };
}

namespace winrt::Misaka::AppEnv::factory_implementation
{
    static class PackageInfo : public  PackageInfoT<PackageInfo, implementation::PackageInfo> {};
}
