// See https://aka.ms/new-console-template for more information

using Device.Net;
using Usb.Net.Windows;

Console.WriteLine("Hello, World!");

/*
// Регистрация фабрики USB-устройств для Windows
WindowsUsbDeviceFactoryExtensions.CreateWindowsUsbDeviceFactory();

// Регистрация фабрики USB-устройств для Windows
WindowsUsbDeviceFactory.Register();

// Получение списка подключенных устройств
var deviceDefinitions = await DeviceManager.Current.GetConnectedDeviceDefinitionsAsync();

// Подключение к первому найденному устройству
var device = await DeviceManager.Current.GetDeviceAsync(deviceDefinitions.First());
*/

var factories = new List<IDeviceFactory>
{
    WindowsUsbDeviceFactoryExtensions.CreateWindowsUsbDeviceFactory(),
    //new WindowsUsbDeviceFactory(),
    //new WindowsHidDeviceFactory()
};

var deviceManager = new DeviceManager(
    notifyDeviceInitialized: async device => await Task.CompletedTask,
    notifyConnectedDevices: async device => await Task.CompletedTask,
    notifyDeviceException: (exception, context) => { /* Обработка исключений */ }, 
    initializeDeviceAction: device => Task.CompletedTask,
    getConnectedDevicesAsync: async () =>
    {
        var devices = new List<ConnectedDeviceDefinition>();
        foreach (var factory in factories)
        {
            devices.AddRange(await factory.GetConnectedDeviceDefinitionsAsync());
        }
        return devices;
    },
    getDevice: async (connectedDeviceDefinition, cancellationToken) => // Added 'cancellationToken' parameter
    {

        //var factory = factories.FirstOrDefault(f => f.GetType().Name.StartsWith(deviceDefinition.DeviceType));
        //return factory == null ? null : await factory.GetDeviceAsync(deviceDefinition);

        foreach (var factory in factories)
        {
            if (await factory.SupportsDeviceAsync(connectedDeviceDefinition, cancellationToken)) // Pass 'cancellationToken'
            {
                return await factory.GetDeviceAsync(connectedDeviceDefinition, cancellationToken); // Pass 'cancellationToken'
            }
        }
        return null;
    },
    pollMilliseconds: 3000,
    loggerFactory: null
);

var devices = await deviceManager._getConnectedDevicesAsync();

foreach (var deviceDefinition in devices)
{
    Console.WriteLine($"Device: {deviceDefinition.DeviceId}, Type: {deviceDefinition.DeviceType}");
}

//deviceManager.Start();

/*
//var deviceManager = new DeviceManager(); 
//deviceManager.RegisterDeviceFactory(new WindowsUsbDeviceFactory());
//deviceManager.RegisterDeviceFactory(new WindowsHidDeviceFactory());
// var devices = await deviceManager.GetConnectedDeviceDefinitionsAsync();

var devices = 


await device.InitializeAsync();

// Взаимодействие с устройством
await device.WriteAsync(new byte[] { 0x00, 0x01 });
var response = await device.ReadAsync();
*/