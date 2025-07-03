using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Device.Net
{
    // Delegates from Device.Net.Reactive

    public delegate void DevicesNotify(IReadOnlyCollection<ConnectedDeviceDefinition> connectedDevices);
    public delegate void DeviceNotify(ConnectedDeviceDefinition connectedDevice);
    public delegate void NotifyDeviceException(ConnectedDeviceDefinition connectedDevice, Exception exception);
    public delegate Task<IReadOnlyList<ConnectedDeviceDefinition>> GetConnectedDevicesAsync();

    // Original delegates from Device.Net

    public delegate Task<IEnumerable<ConnectedDeviceDefinition>> GetConnectedDeviceDefinitionsAsync(CancellationToken cancellationToken = default);
    public delegate ConnectedDeviceDefinition GetDeviceDefinition(string deviceId, Guid classGuid);
    public delegate Task<IDevice> GetDeviceAsync(ConnectedDeviceDefinition deviceId, CancellationToken cancellationToken = default);
}
