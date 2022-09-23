using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon.Authorization
{
    public enum AccountPermission
    {
        ADMINISTRATOR,
        CREATE_RELEASE,
        READ_RELEASE_BYPASS,
        DELETE_RELEASE,
        DELETE_ACCOUNT,
        CONFIG_READ,
        CONFIG_WRITE,
        USER_GROUP_MODIFY,
        USER_PERMISSION_MODIFY,
        USER_TOKEN_PURGE,
        USER_LIST
    }
}
