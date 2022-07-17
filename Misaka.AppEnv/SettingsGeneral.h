#pragma once

#include "SettingsGeneral.g.h"
#include "SettingsBase.h"

namespace winrt::Misaka::AppEnv::implementation
{
    struct SettingsGeneral : SettingsGeneralT<SettingsGeneral, Misaka::AppEnv::implementation::SettingsBase>
    {
    public:
        SettingsGeneral();
        bool MinimizeToNotificationArea();
        void MinimizeToNotificationArea(bool const& value);
        bool PushNotification();
        void PushNotification(bool const& value);
    };
}

namespace winrt::Misaka::AppEnv::factory_implementation
{
    struct SettingsGeneral : SettingsGeneralT<SettingsGeneral, implementation::SettingsGeneral> {};
}
