using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.Common.Enums
{
    public enum EventTypeEnum
    {
        InitialInventory = 1,
        ManuallyAdded = 2,
        ManuallyRemoved = 3,
        TransferReceived = 4,
        TransferSent = 5,
        ManuallySet = 6
    }
}
