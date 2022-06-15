using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Essential.Interfaces;
using Microsoft.Maui.Accessibility;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Authentication;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Media;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using Xamarin.Essentials.Interfaces;

namespace Xamarin.Essentials.Implementation
{
    public class AccelerometerImplementation : Xamarin.Essentials.Interfaces.IAccelerometer, Microsoft.Maui.Devices.Sensors.IAccelerometer, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public AccelerometerImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IAccelerometer _accelerometerImplementation
            => Accelerometer.Default;

        public void Start(SensorSpeed sensorSpeed) =>
            _accelerometerImplementation.Start(sensorSpeed);

        public void Stop() =>
            _accelerometerImplementation.Stop();

        public bool IsSupported => _accelerometerImplementation.IsSupported;

        public bool IsMonitoring => _accelerometerImplementation.IsMonitoring;

        public event EventHandler<AccelerometerChangedEventArgs> ReadingChanged
        {
            add => _accelerometerImplementation.ReadingChanged += value;
            remove => _accelerometerImplementation.ReadingChanged -= value;
        }

        public event EventHandler ShakeDetected
        {
            add => _accelerometerImplementation.ShakeDetected += value;
            remove => _accelerometerImplementation.ShakeDetected -= value;
        }
    }

    public class AppActionsImplementation : Xamarin.Essentials.Interfaces.IAppActions, Microsoft.Maui.ApplicationModel.IAppActions, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public AppActionsImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.IAppActions _appActionsImplementation
            => AppActions.Current;

        public Task<IEnumerable<AppAction>> GetAsync() =>
            _appActionsImplementation.GetAsync();

        public Task SetAsync(params AppAction[] actions) =>
            _appActionsImplementation.SetAsync(actions);

        public Task SetAsync(IEnumerable<AppAction> actions) =>
            _appActionsImplementation.SetAsync(actions);

        public bool IsSupported => _appActionsImplementation.IsSupported;

        public event EventHandler<AppActionEventArgs> AppActionActivated
        {
            add => _appActionsImplementation.AppActionActivated += value;
            remove => _appActionsImplementation.AppActionActivated -= value;
        }

        public event EventHandler<AppActionEventArgs> OnAppAction
        {
            add => _appActionsImplementation.AppActionActivated += value;
            remove => _appActionsImplementation.AppActionActivated -= value;
        }
    }

    public class AppInfoImplementation : Xamarin.Essentials.Interfaces.IAppInfo, Microsoft.Maui.ApplicationModel.IAppInfo, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public AppInfoImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.IAppInfo _appInfoImplementation
            => AppInfo.Current;

        public void ShowSettingsUI() =>
            _appInfoImplementation.ShowSettingsUI();

        public string PackageName => _appInfoImplementation.PackageName;
        public string Name => _appInfoImplementation.Name;
        public string VersionString => _appInfoImplementation.VersionString;
        public Version Version => _appInfoImplementation.Version;
        public string BuildString => _appInfoImplementation.BuildString;

        AppTheme Microsoft.Maui.ApplicationModel.IAppInfo.RequestedTheme => _appInfoImplementation.RequestedTheme;
        public AppPackagingModel PackagingModel => _appInfoImplementation.PackagingModel;
        public LayoutDirection RequestedLayoutDirection => _appInfoImplementation.RequestedLayoutDirection;
        public AppTheme RequestedTheme => _appInfoImplementation.RequestedTheme;
    }

    public class BarometerImplementation : Xamarin.Essentials.Interfaces.IBarometer, Microsoft.Maui.Devices.Sensors.IBarometer, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public BarometerImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IBarometer _barometerImplementation
            => Barometer.Default;

        public void Start(SensorSpeed sensorSpeed) =>
            _barometerImplementation.Start(sensorSpeed);

        public void Stop() =>
            _barometerImplementation.Stop();

        public bool IsSupported => _barometerImplementation.IsSupported;
        public bool IsMonitoring => _barometerImplementation.IsMonitoring;

        public event EventHandler<BarometerChangedEventArgs> ReadingChanged
        {
            add => _barometerImplementation.ReadingChanged += value;
            remove => _barometerImplementation.ReadingChanged -= value;
        }
    }

    public class BatteryImplementation : Xamarin.Essentials.Interfaces.IBattery, Microsoft.Maui.Devices.IBattery, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public BatteryImplementation()
        {
        }

        private Microsoft.Maui.Devices.IBattery _batteryImplementation
            => Battery.Default;

        public double ChargeLevel => _batteryImplementation.ChargeLevel;
        public BatteryState State => _batteryImplementation.State;
        public BatteryPowerSource PowerSource => _batteryImplementation.PowerSource;
        public EnergySaverStatus EnergySaverStatus => _batteryImplementation.EnergySaverStatus;

        public event EventHandler<BatteryInfoChangedEventArgs> BatteryInfoChanged
        {
            add => _batteryImplementation.BatteryInfoChanged += value;
            remove => _batteryImplementation.BatteryInfoChanged -= value;
        }

        public event EventHandler<EnergySaverStatusChangedEventArgs> EnergySaverStatusChanged
        {
            add => _batteryImplementation.EnergySaverStatusChanged += value;
            remove => _batteryImplementation.EnergySaverStatusChanged -= value;
        }
    }

    public class BrowserImplementation : Xamarin.Essentials.Interfaces.IBrowser, Microsoft.Maui.ApplicationModel.IBrowser, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public BrowserImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.IBrowser _browserImplementation
            => Browser.Default;

        public Task OpenAsync(string uri) =>
            _browserImplementation.OpenAsync(uri);

        public Task OpenAsync(string uri, BrowserLaunchMode launchMode) =>
            _browserImplementation.OpenAsync(uri, launchMode);

        public Task OpenAsync(string uri, BrowserLaunchOptions options) =>
            _browserImplementation.OpenAsync(uri, options);

        public Task OpenAsync(Uri uri) =>
            _browserImplementation.OpenAsync(uri);

        public Task OpenAsync(Uri uri, BrowserLaunchMode launchMode) =>
            _browserImplementation.OpenAsync(uri, launchMode);

        public Task<bool> OpenAsync(Uri uri, BrowserLaunchOptions options) =>
            _browserImplementation.OpenAsync(uri, options);
    }

    public class ClipboardImplementation : Xamarin.Essentials.Interfaces.IClipboard, Microsoft.Maui.ApplicationModel.DataTransfer.IClipboard, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public ClipboardImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.DataTransfer.IClipboard _clipboardImplementation
            => Clipboard.Default;

        public Task SetTextAsync(string text) =>
            _clipboardImplementation.SetTextAsync(text);

        public Task<string> GetTextAsync() =>
            _clipboardImplementation.GetTextAsync();

        public bool HasText => _clipboardImplementation.HasText;

        public event EventHandler<EventArgs> ClipboardContentChanged
        {
            add => _clipboardImplementation.ClipboardContentChanged += value;
            remove => _clipboardImplementation.ClipboardContentChanged -= value;
        }
    }

    public class CompassImplementation : Xamarin.Essentials.Interfaces.ICompass, Microsoft.Maui.Devices.Sensors.ICompass, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public CompassImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.ICompass _compassImplementation
            => Compass.Default;

        public void Start(SensorSpeed sensorSpeed) =>
            _compassImplementation.Start(sensorSpeed);

        public void Start(SensorSpeed sensorSpeed, bool applyLowPassFilter) =>
            _compassImplementation.Start(sensorSpeed, applyLowPassFilter);

        public void Stop() =>
            _compassImplementation.Stop();

        public bool IsSupported => _compassImplementation.IsSupported;
        public bool IsMonitoring => _compassImplementation.IsMonitoring;

        public event EventHandler<CompassChangedEventArgs> ReadingChanged
        {
            add => _compassImplementation.ReadingChanged += value;
            remove => _compassImplementation.ReadingChanged -= value;
        }
    }

    public class ConnectivityImplementation : Xamarin.Essentials.Interfaces.IConnectivity, Microsoft.Maui.Networking.IConnectivity, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public ConnectivityImplementation()
        {
        }

        private Microsoft.Maui.Networking.IConnectivity _connectivityImplementation
            => Connectivity.Current;

        public IEnumerable<ConnectionProfile> ConnectionProfiles => _connectivityImplementation.ConnectionProfiles;
        public NetworkAccess NetworkAccess => _connectivityImplementation.NetworkAccess;

        public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged
        {
            add => _connectivityImplementation.ConnectivityChanged += value;
            remove => _connectivityImplementation.ConnectivityChanged -= value;
        }
    }

    public class ContactsImplementation : Xamarin.Essentials.Interfaces.IContacts, Microsoft.Maui.ApplicationModel.Communication.IContacts, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public ContactsImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.Communication.IContacts _contactsImplementation
            => Microsoft.Maui.ApplicationModel.Communication.Contacts.Default;

        public Task<Contact> PickContactAsync() =>
            _contactsImplementation.PickContactAsync();

        public Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken = new CancellationToken()) =>
            _contactsImplementation.GetAllAsync(cancellationToken);
    }

    public class DeviceDisplayImplementation : Xamarin.Essentials.Interfaces.IDeviceDisplay, Microsoft.Maui.Devices.IDeviceDisplay, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public DeviceDisplayImplementation()
        {
        }

        private Microsoft.Maui.Devices.IDeviceDisplay _deviceDisplayImplementation
            => DeviceDisplay.Current;

        public bool KeepScreenOn
        {
            get => _deviceDisplayImplementation.KeepScreenOn;
            set => _deviceDisplayImplementation.KeepScreenOn = value;
        }

        public DisplayInfo MainDisplayInfo => _deviceDisplayImplementation.MainDisplayInfo;

        public event EventHandler<DisplayInfoChangedEventArgs> MainDisplayInfoChanged
        {
            add => _deviceDisplayImplementation.MainDisplayInfoChanged += value;
            remove => _deviceDisplayImplementation.MainDisplayInfoChanged -= value;
        }
    }

    public class DeviceInfoImplementation : Xamarin.Essentials.Interfaces.IDeviceInfo, Microsoft.Maui.Devices.IDeviceInfo, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public DeviceInfoImplementation()
        {
        }

        private Microsoft.Maui.Devices.IDeviceInfo _deviceInfoImplementation
            => DeviceInfo.Current;

        public string Model => _deviceInfoImplementation.Model;
        public string Manufacturer => _deviceInfoImplementation.Manufacturer;
        public string Name => _deviceInfoImplementation.Name;
        public string VersionString => _deviceInfoImplementation.VersionString;
        public Version Version => _deviceInfoImplementation.Version;
        public DevicePlatform Platform => _deviceInfoImplementation.Platform;
        public DeviceIdiom Idiom => _deviceInfoImplementation.Idiom;
        public DeviceType DeviceType => _deviceInfoImplementation.DeviceType;
    }

    public class EmailImplementation : Xamarin.Essentials.Interfaces.IEmail, Microsoft.Maui.ApplicationModel.Communication.IEmail, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public EmailImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.Communication.IEmail _emailImplementation
            => Email.Default;

        public Task ComposeAsync() =>
            _emailImplementation.ComposeAsync();

        public Task ComposeAsync(string subject, string body, params string[] to) =>
            _emailImplementation.ComposeAsync(subject, body, to);

        public Task ComposeAsync(EmailMessage message) =>
            _emailImplementation.ComposeAsync(message);

        public bool IsComposeSupported => _emailImplementation.IsComposeSupported;
    }

    public class FilePickerImplementation : Xamarin.Essentials.Interfaces.IFilePicker, Microsoft.Maui.Storage.IFilePicker, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public FilePickerImplementation()
        {
        }

        private Microsoft.Maui.Storage.IFilePicker _filePickerImplementation
            => FilePicker.Default;

        public Task<FileResult> PickAsync(PickOptions options = null)
            => _filePickerImplementation.PickAsync(options);

        public Task<IEnumerable<FileResult>> PickMultipleAsync(PickOptions options = null)
            => _filePickerImplementation.PickMultipleAsync(options);
    }

    public class FileSystemImplementation : Xamarin.Essentials.Interfaces.IFileSystem, Microsoft.Maui.Storage.IFileSystem, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public FileSystemImplementation()
        {
        }

        private Microsoft.Maui.Storage.IFileSystem _fileSystemImplementation
            => FileSystem.Current;

        public Task<Stream> OpenAppPackageFileAsync(string filename) =>
            _fileSystemImplementation.OpenAppPackageFileAsync(filename);

        public Task<bool> AppPackageFileExistsAsync(string filename) =>
            _fileSystemImplementation.AppPackageFileExistsAsync(filename);

        public string CacheDirectory => _fileSystemImplementation.CacheDirectory;
        public string AppDataDirectory => _fileSystemImplementation.AppDataDirectory;
    }

    public class FlashlightImplementation : Xamarin.Essentials.Interfaces.IFlashlight, Microsoft.Maui.Devices.IFlashlight, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public FlashlightImplementation()
        {
        }

        private Microsoft.Maui.Devices.IFlashlight _flashlightImplementation
            => Flashlight.Default;

        public Task TurnOnAsync() =>
            _flashlightImplementation.TurnOnAsync();

        public Task TurnOffAsync() =>
            _flashlightImplementation.TurnOffAsync();
    }

    public class GeocodingImplementation : Xamarin.Essentials.Interfaces.IGeocoding, Microsoft.Maui.Devices.Sensors.IGeocoding, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public GeocodingImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IGeocoding _geocodingImplementation
            => Geocoding.Default;

        public Task<IEnumerable<Placemark>> GetPlacemarksAsync(Location location)
            => _geocodingImplementation.GetPlacemarksAsync(location);

        public Task<IEnumerable<Placemark>> GetPlacemarksAsync(double latitude, double longitude) =>
            _geocodingImplementation.GetPlacemarksAsync(latitude, longitude);

        public Task<IEnumerable<Location>> GetLocationsAsync(string address) =>
            _geocodingImplementation.GetLocationsAsync(address);
    }

    public class GeolocationImplementation : Xamarin.Essentials.Interfaces.IGeolocation, Microsoft.Maui.Devices.Sensors.IGeolocation, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public GeolocationImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IGeolocation _geolocationImplementation
            => Geolocation.Default;

        public Task<Location> GetLastKnownLocationAsync() =>
            _geolocationImplementation.GetLastKnownLocationAsync();

        public Task<Location> GetLocationAsync() =>
            _geolocationImplementation.GetLocationAsync();

        public Task<Location> GetLocationAsync(GeolocationRequest request)
            => _geolocationImplementation.GetLocationAsync(request);

        public Task<Location> GetLocationAsync(GeolocationRequest request, CancellationToken cancelToken) =>
            _geolocationImplementation.GetLocationAsync(request, cancelToken);
    }

    public class GyroscopeImplementation : Xamarin.Essentials.Interfaces.IGyroscope, Microsoft.Maui.Devices.Sensors.IGyroscope, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public GyroscopeImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IGyroscope _gyroscopeImplementation
            => Gyroscope.Default;

        public void Start(SensorSpeed sensorSpeed) =>
            _gyroscopeImplementation.Start(sensorSpeed);

        public void Stop() =>
            _gyroscopeImplementation.Stop();

        public bool IsSupported => _gyroscopeImplementation.IsSupported;
        public bool IsMonitoring => _gyroscopeImplementation.IsMonitoring;

        public event EventHandler<GyroscopeChangedEventArgs> ReadingChanged
        {
            add => _gyroscopeImplementation.ReadingChanged += value;
            remove => _gyroscopeImplementation.ReadingChanged -= value;
        }
    }

    public class HapticFeedbackImplementation : Xamarin.Essentials.Interfaces.IHapticFeedback, Microsoft.Maui.Devices.IHapticFeedback, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public HapticFeedbackImplementation()
        {
        }

        private Microsoft.Maui.Devices.IHapticFeedback _hapticFeedbackImplementation
            => HapticFeedback.Default;

        public void Perform(HapticFeedbackType type) =>
            _hapticFeedbackImplementation.Perform(type);

        public bool IsSupported => _hapticFeedbackImplementation.IsSupported;
    }

    public class LauncherImplementation : Xamarin.Essentials.Interfaces.ILauncher, Microsoft.Maui.ApplicationModel.ILauncher, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public LauncherImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.ILauncher _launcherImplementation
            => Launcher.Default;

        public Task<bool> CanOpenAsync(string uri) =>
            _launcherImplementation.CanOpenAsync(uri);

        public Task<bool> CanOpenAsync(Uri uri) =>
            _launcherImplementation.CanOpenAsync(uri);

        public Task OpenAsync(string uri) =>
            _launcherImplementation.OpenAsync(uri);

        Task Xamarin.Essentials.Interfaces.ILauncher.OpenAsync(Uri uri) =>
            _launcherImplementation.OpenAsync(uri);

        Task Xamarin.Essentials.Interfaces.ILauncher.OpenAsync(OpenFileRequest request) =>
            _launcherImplementation.OpenAsync(request);

        public Task<bool> TryOpenAsync(string uri) =>
            _launcherImplementation.TryOpenAsync(uri);

        public Task<bool> OpenAsync(Uri uri) =>
            _launcherImplementation.OpenAsync(uri);

        public Task<bool> OpenAsync(OpenFileRequest request) =>
            _launcherImplementation.OpenAsync(request);

        public Task<bool> TryOpenAsync(Uri uri) =>
            _launcherImplementation.TryOpenAsync(uri);
    }

    public class MagnetometerImplementation : Xamarin.Essentials.Interfaces.IMagnetometer, Microsoft.Maui.Devices.Sensors.IMagnetometer, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public MagnetometerImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IMagnetometer _magnetometerImplementation
            => Magnetometer.Default;

        public void Start(SensorSpeed sensorSpeed) =>
            _magnetometerImplementation.Start(sensorSpeed);

        public void Stop() =>
            _magnetometerImplementation.Stop();

        public bool IsSupported => _magnetometerImplementation.IsSupported;
        public bool IsMonitoring => _magnetometerImplementation.IsMonitoring;

        public event EventHandler<MagnetometerChangedEventArgs> ReadingChanged
        {
            add => _magnetometerImplementation.ReadingChanged += value;
            remove => _magnetometerImplementation.ReadingChanged -= value;
        }
    }

    [Obsolete]
    public class MainThreadImplementation : Xamarin.Essentials.Interfaces.IMainThread, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public MainThreadImplementation()
        {
        }

        [Obsolete]
        public void BeginInvokeOnMainThread(Action action)
            => Microsoft.Maui.Controls.Device.BeginInvokeOnMainThread(action);

        [Obsolete]
        public Task InvokeOnMainThreadAsync(Action action)
            => Microsoft.Maui.Controls.Device.InvokeOnMainThreadAsync(action);

        [Obsolete]
        public Task<T> InvokeOnMainThreadAsync<T>(Func<T> func)
            => Microsoft.Maui.Controls.Device.InvokeOnMainThreadAsync(func);

        [Obsolete]
        public Task InvokeOnMainThreadAsync(Func<Task> funcTask)
            => Microsoft.Maui.Controls.Device.InvokeOnMainThreadAsync(funcTask);

        [Obsolete]
        public Task<T> InvokeOnMainThreadAsync<T>(Func<Task<T>> funcTask)
            => Microsoft.Maui.Controls.Device.InvokeOnMainThreadAsync(funcTask);

        [Obsolete]
        public Task<SynchronizationContext> GetMainThreadSynchronizationContextAsync()
            => Microsoft.Maui.Controls.Device.GetMainThreadSynchronizationContextAsync();

        [Obsolete]
        public bool IsMainThread
            => !Microsoft.Maui.Controls.Device.IsInvokeRequired;
    }

    public class MapImplementation : Xamarin.Essentials.Interfaces.IMap, Microsoft.Maui.ApplicationModel.IMap, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public MapImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.IMap _mapImplementation
            => Map.Default;

        public Task OpenAsync(Location location) =>
            _mapImplementation.OpenAsync(location);

        public Task OpenAsync(Location location, MapLaunchOptions options) =>
            _mapImplementation.OpenAsync(location, options);

        public Task OpenAsync(double latitude, double longitude) =>
            _mapImplementation.OpenAsync(latitude, longitude);

        public Task OpenAsync(double latitude, double longitude, MapLaunchOptions options) =>
            _mapImplementation.OpenAsync(latitude, longitude, options);

        public Task OpenAsync(Placemark placemark) =>
            _mapImplementation.OpenAsync(placemark);

        public Task OpenAsync(Placemark placemark, MapLaunchOptions options) =>
            _mapImplementation.OpenAsync(placemark, options);

        // shrug
        public async Task<bool> TryOpenAsync(double latitude, double longitude, MapLaunchOptions options)
        {
            try { await _mapImplementation.OpenAsync(latitude, longitude, options); return true; }
            catch { return false; }
        }

        // shrug
        public async Task<bool> TryOpenAsync(Placemark placemark, MapLaunchOptions options)
        {
            try { await _mapImplementation.OpenAsync(placemark, options); return true; }
            catch { return false; }
        }
    }

    public class MediaPickerImplementation : Xamarin.Essentials.Interfaces.IMediaPicker, Microsoft.Maui.Media.IMediaPicker, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public MediaPickerImplementation()
        {
        }

        private Microsoft.Maui.Media.IMediaPicker _mediaPickerImplementation
            => MediaPicker.Default;

        public Task<FileResult> PickPhotoAsync(MediaPickerOptions options = null) =>
            _mediaPickerImplementation.PickPhotoAsync(options);

        public Task<FileResult> CapturePhotoAsync(MediaPickerOptions options = null) =>
            _mediaPickerImplementation.CapturePhotoAsync(options);

        public Task<FileResult> PickVideoAsync(MediaPickerOptions options = null) =>
            _mediaPickerImplementation.PickVideoAsync(options);

        public Task<FileResult> CaptureVideoAsync(MediaPickerOptions options = null) =>
            _mediaPickerImplementation.CaptureVideoAsync(options);

        public bool IsCaptureSupported => _mediaPickerImplementation.IsCaptureSupported;
    }

    public class OrientationSensorImplementation : Xamarin.Essentials.Interfaces.IOrientationSensor, Microsoft.Maui.Devices.Sensors.IOrientationSensor, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public OrientationSensorImplementation()
        {
        }

        private Microsoft.Maui.Devices.Sensors.IOrientationSensor _orientationSensorImplementation
            => OrientationSensor.Default;

        public void Start(SensorSpeed sensorSpeed) =>
            _orientationSensorImplementation.Start(sensorSpeed);

        public void Stop() =>
            _orientationSensorImplementation.Stop();

        public bool IsSupported => _orientationSensorImplementation.IsSupported;
        public bool IsMonitoring => _orientationSensorImplementation.IsMonitoring;

        public event EventHandler<OrientationSensorChangedEventArgs> ReadingChanged
        {
            add => _orientationSensorImplementation.ReadingChanged += value;
            remove => _orientationSensorImplementation.ReadingChanged -= value;
        }
    }

    // no maui interface?
    public class PermissionsImplementation : IPermissions, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public PermissionsImplementation()
        {
        }

        public Task<PermissionStatus> CheckStatusAsync<TPermission>()
            where TPermission : Permissions.BasePermission, new()
            => Permissions.CheckStatusAsync<TPermission>();

        public Task<PermissionStatus> RequestAsync<TPermission>() where TPermission : Permissions.BasePermission, new()
            => Permissions.RequestAsync<TPermission>();

        public bool ShouldShowRationale<TPermission>() where TPermission : Permissions.BasePermission, new()
            => Permissions.ShouldShowRationale<TPermission>();
    }

    public class PhoneDialerImplementation : Xamarin.Essentials.Interfaces.IPhoneDialer, Microsoft.Maui.ApplicationModel.Communication.IPhoneDialer, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public PhoneDialerImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.Communication.IPhoneDialer _phoneDialerImplementation
            => PhoneDialer.Default;

        public void Open(string number) =>
            _phoneDialerImplementation.Open(number);

        public bool IsSupported => _phoneDialerImplementation.IsSupported;
    }

    public class PreferencesImplementation : Xamarin.Essentials.Interfaces.IPreferences, Microsoft.Maui.Storage.IPreferences, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public PreferencesImplementation()
        {
        }

        private Microsoft.Maui.Storage.IPreferences _preferencesImplementation
            => Preferences.Default;

        public bool ContainsKey(string key) =>
            _preferencesImplementation.ContainsKey(key);

        public void Remove(string key) =>
            _preferencesImplementation.Remove(key);

        public void Clear() =>
            _preferencesImplementation.Clear();

        public string Get(string key, string defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public bool Get(string key, bool defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public int Get(string key, int defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public double Get(string key, double defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public float Get(string key, float defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public long Get(string key, long defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public void Set(string key, string value) =>
            _preferencesImplementation.Set(key, value);

        public void Set(string key, bool value) =>
            _preferencesImplementation.Set(key, value);

        public void Set(string key, int value) =>
            _preferencesImplementation.Set(key, value);

        public void Set(string key, double value) =>
            _preferencesImplementation.Set(key, value);

        public void Set(string key, float value) =>
            _preferencesImplementation.Set(key, value);

        public void Set(string key, long value) =>
            _preferencesImplementation.Set(key, value);

        public bool ContainsKey(string key, string sharedName = null) =>
            _preferencesImplementation.ContainsKey(key, sharedName);

        public void Remove(string key, string sharedName = null) =>
            _preferencesImplementation.Remove(key, sharedName);

        public void Clear(string sharedName = null) =>
            _preferencesImplementation.Clear(sharedName);

        public string Get(string key, string defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public bool Get(string key, bool defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public int Get(string key, int defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public double Get(string key, double defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public float Get(string key, float defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public long Get(string key, long defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public void Set(string key, string value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public void Set(string key, bool value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public void Set(string key, int value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public void Set(string key, double value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public void Set(string key, float value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public void Set(string key, long value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public DateTime Get(string key, DateTime defaultValue) =>
            _preferencesImplementation.Get(key, defaultValue);

        public void Set(string key, DateTime value) =>
            _preferencesImplementation.Set(key, value);

        public DateTime Get(string key, DateTime defaultValue, string sharedName) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);

        public void Set(string key, DateTime value, string sharedName) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public void Set<T>(string key, T value, string sharedName = null) =>
            _preferencesImplementation.Set(key, value, sharedName);

        public T Get<T>(string key, T defaultValue, string sharedName = null) =>
            _preferencesImplementation.Get(key, defaultValue, sharedName);
    }

    public class ScreenshotImplementation : Xamarin.Essentials.Interfaces.IScreenshot, Microsoft.Maui.Media.IScreenshot, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public ScreenshotImplementation()
        {
        }

        private Microsoft.Maui.Media.IScreenshot _screenshotImplementation
            => Screenshot.Default;

        public Task<IScreenshotResult> CaptureAsync() =>
            _screenshotImplementation.CaptureAsync();

        public bool IsCaptureSupported => _screenshotImplementation.IsCaptureSupported;
    }

    public class SecureStorageImplementation : Xamarin.Essentials.Interfaces.ISecureStorage, Microsoft.Maui.Storage.ISecureStorage, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public SecureStorageImplementation()
        {
        }

        private Microsoft.Maui.Storage.ISecureStorage _secureStorageImplementation
            => SecureStorage.Default;

        public Task<string> GetAsync(string key) =>
            _secureStorageImplementation.GetAsync(key);

        public Task SetAsync(string key, string value) =>
            _secureStorageImplementation.SetAsync(key, value);

        public bool Remove(string key) =>
            _secureStorageImplementation.Remove(key);

        public void RemoveAll() =>
            _secureStorageImplementation.RemoveAll();
    }

    public class ShareImplementation : Xamarin.Essentials.Interfaces.IShare, Microsoft.Maui.ApplicationModel.DataTransfer.IShare, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public ShareImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.DataTransfer.IShare _shareImplementation
            => Share.Default;

        public Task RequestAsync(string text) =>
            _shareImplementation.RequestAsync(text);

        public Task RequestAsync(string text, string title) =>
            _shareImplementation.RequestAsync(text, title);

        public Task RequestAsync(ShareTextRequest request) =>
            _shareImplementation.RequestAsync(request);

        public Task RequestAsync(ShareFileRequest request) =>
            _shareImplementation.RequestAsync(request);

        public Task RequestAsync(ShareMultipleFilesRequest request) =>
            _shareImplementation.RequestAsync(request);
    }

    public class SmsImplementation : Xamarin.Essentials.Interfaces.ISms, Microsoft.Maui.ApplicationModel.Communication.ISms, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public SmsImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.Communication.ISms _smsImplementation
            => Sms.Default;

        public Task ComposeAsync() =>
            _smsImplementation.ComposeAsync(default);

        public Task ComposeAsync(SmsMessage message) =>
            _smsImplementation.ComposeAsync(message);

        public bool IsComposeSupported => _smsImplementation.IsComposeSupported;
    }

    public class TextToSpeechImplementation : Xamarin.Essentials.Interfaces.ITextToSpeech, Microsoft.Maui.Media.ITextToSpeech, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public TextToSpeechImplementation()
        {
        }

        private Microsoft.Maui.Media.ITextToSpeech _textToSpeechImplementation
            => TextToSpeech.Default;

        public Task<IEnumerable<Locale>> GetLocalesAsync() =>
            _textToSpeechImplementation.GetLocalesAsync();

        public Task SpeakAsync(string text, CancellationToken cancelToken = default)
            => _textToSpeechImplementation.SpeakAsync(text, default, cancelToken);

        public Task SpeakAsync(string text, SpeechOptions options = null, CancellationToken cancelToken = default) =>
            _textToSpeechImplementation.SpeakAsync(text, options, cancelToken);
    }

    public class VersionTrackingImplementation : Xamarin.Essentials.Interfaces.IVersionTracking, Microsoft.Maui.ApplicationModel.IVersionTracking, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public VersionTrackingImplementation()
        {
        }

        private Microsoft.Maui.ApplicationModel.IVersionTracking _versionTrackingImplementation
            => VersionTracking.Default;

        public void Track() =>
            _versionTrackingImplementation.Track();

        public bool IsFirstLaunchForVersion(string version) =>
            _versionTrackingImplementation.IsFirstLaunchForVersion(version);

        public bool IsFirstLaunchForBuild(string build) =>
            _versionTrackingImplementation.IsFirstLaunchForBuild(build);

        public bool IsFirstLaunchEver => _versionTrackingImplementation.IsFirstLaunchEver;
        public bool IsFirstLaunchForCurrentVersion => _versionTrackingImplementation.IsFirstLaunchForCurrentVersion;
        public bool IsFirstLaunchForCurrentBuild => _versionTrackingImplementation.IsFirstLaunchForCurrentBuild;
        public string CurrentVersion => _versionTrackingImplementation.CurrentVersion;
        public string CurrentBuild => _versionTrackingImplementation.CurrentBuild;
        public string PreviousVersion => _versionTrackingImplementation.PreviousVersion;
        public string PreviousBuild => _versionTrackingImplementation.PreviousBuild;
        public string FirstInstalledVersion => _versionTrackingImplementation.FirstInstalledVersion;
        public string FirstInstalledBuild => _versionTrackingImplementation.FirstInstalledBuild;

        IEnumerable<string> Xamarin.Essentials.Interfaces.IVersionTracking.VersionHistory => _versionTrackingImplementation.VersionHistory;
        IEnumerable<string> Xamarin.Essentials.Interfaces.IVersionTracking.BuildHistory => _versionTrackingImplementation.BuildHistory;

        public IReadOnlyList<string> VersionHistory => _versionTrackingImplementation.VersionHistory;
        public IReadOnlyList<string> BuildHistory => _versionTrackingImplementation.BuildHistory;
    }

    public class VibrationImplementation : Xamarin.Essentials.Interfaces.IVibration, Microsoft.Maui.Devices.IVibration, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public VibrationImplementation()
        {
        }

        private Microsoft.Maui.Devices.IVibration _vibrationImplementation
            => Vibration.Default;

        public void Vibrate() =>
            _vibrationImplementation.Vibrate();

        public void Vibrate(double duration) =>
            _vibrationImplementation.Vibrate(duration);

        public void Vibrate(TimeSpan duration) =>
            _vibrationImplementation.Vibrate(duration);

        public void Cancel() =>
            _vibrationImplementation.Cancel();

        public bool IsSupported => _vibrationImplementation.IsSupported;
    }

    public class WebAuthenticatorImplementation : Xamarin.Essentials.Interfaces.IWebAuthenticator, Microsoft.Maui.Authentication.IWebAuthenticator, IEssentialsImplementation
    {
        [Preserve(Conditional = true)]
        public WebAuthenticatorImplementation()
        {
        }

        private Microsoft.Maui.Authentication.IWebAuthenticator _webAuthenticatorImplementation
            => WebAuthenticator.Default;

        public Task<WebAuthenticatorResult> AuthenticateAsync(Uri url, Uri callbackUrl) =>
            _webAuthenticatorImplementation.AuthenticateAsync(url, callbackUrl);

        public Task<WebAuthenticatorResult> AuthenticateAsync(WebAuthenticatorOptions webAuthenticatorOptions) =>
            _webAuthenticatorImplementation.AuthenticateAsync(webAuthenticatorOptions);
    }
}

namespace Xamarin.Essentials.Interfaces
{
    public interface IAccelerometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
        event EventHandler<AccelerometerChangedEventArgs> ReadingChanged;
        event EventHandler ShakeDetected;
    }

    public interface IAppActions
    {
        Task<IEnumerable<AppAction>> GetAsync();
        Task SetAsync(params AppAction[] actions);
        Task SetAsync(IEnumerable<AppAction> actions);
        event EventHandler<AppActionEventArgs> OnAppAction;
    }

    public interface IAppInfo
    {
        void ShowSettingsUI();
        string PackageName { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        string BuildString { get; }
        AppTheme RequestedTheme { get; }
    }

    public interface IBarometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
        event EventHandler<BarometerChangedEventArgs> ReadingChanged;
    }

    public interface IBattery
    {
        double ChargeLevel { get; }
        BatteryState State { get; }
        BatteryPowerSource PowerSource { get; }
        EnergySaverStatus EnergySaverStatus { get; }
        event EventHandler<BatteryInfoChangedEventArgs> BatteryInfoChanged;
        event EventHandler<EnergySaverStatusChangedEventArgs> EnergySaverStatusChanged;
    }

    public interface IBrowser
    {
        Task OpenAsync(string uri);
        Task OpenAsync(string uri, BrowserLaunchMode launchMode);
        Task OpenAsync(string uri, BrowserLaunchOptions options);
        Task OpenAsync(Uri uri);
        Task OpenAsync(Uri uri, BrowserLaunchMode launchMode);
        Task<bool> OpenAsync(Uri uri, BrowserLaunchOptions options);
    }

    public interface IClipboard
    {
        Task SetTextAsync(string text);
        Task<string> GetTextAsync();
        bool HasText { get; }
        event EventHandler<EventArgs> ClipboardContentChanged;
    }

    public interface ICompass
    {
        void Start(SensorSpeed sensorSpeed);
        void Start(SensorSpeed sensorSpeed, bool applyLowPassFilter);
        void Stop();
        bool IsMonitoring { get; }
        event EventHandler<CompassChangedEventArgs> ReadingChanged;
    }

    public interface IConnectivity
    {
        NetworkAccess NetworkAccess { get; }
        IEnumerable<ConnectionProfile> ConnectionProfiles { get; }
        event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;
    }

    public interface IContacts
    {
        Task<Contact> PickContactAsync();
        Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken = default);
    }

    public interface IDeviceDisplay
    {
        bool KeepScreenOn { get; set; }
        DisplayInfo MainDisplayInfo { get; }
        event EventHandler<DisplayInfoChangedEventArgs> MainDisplayInfoChanged;
    }

    public interface IDeviceInfo
    {
        string Model { get; }
        string Manufacturer { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        DevicePlatform Platform { get; }
        DeviceIdiom Idiom { get; }
        DeviceType DeviceType { get; }
    }

    public interface IEmail
    {
        Task ComposeAsync();
        Task ComposeAsync(string subject, string body, params string[] to);
        Task ComposeAsync(EmailMessage message);
    }

    public interface IFilePicker
    {
        Task<FileResult> PickAsync(PickOptions options = null);
        Task<IEnumerable<FileResult>> PickMultipleAsync(PickOptions options = null);
    }

    public interface IFileSystem
    {
        Task<Stream> OpenAppPackageFileAsync(string filename);
        string CacheDirectory { get; }
        string AppDataDirectory { get; }
    }

    public interface IFlashlight
    {
        Task TurnOnAsync();
        Task TurnOffAsync();
    }

    public interface IGeocoding
    {
        Task<IEnumerable<Placemark>> GetPlacemarksAsync(Location location);
        Task<IEnumerable<Placemark>> GetPlacemarksAsync(double latitude, double longitude);
        Task<IEnumerable<Location>> GetLocationsAsync(string address);
    }

    public interface IGeolocation
    {
        Task<Location> GetLastKnownLocationAsync();
        Task<Location> GetLocationAsync();
        Task<Location> GetLocationAsync(GeolocationRequest request);
        Task<Location> GetLocationAsync(GeolocationRequest request, CancellationToken cancelToken);
    }

    public interface IGyroscope
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
        event EventHandler<GyroscopeChangedEventArgs> ReadingChanged;
    }

    public interface IHapticFeedback
    {
        void Perform(HapticFeedbackType type = HapticFeedbackType.Click);
    }

    public interface ILauncher
    {
        Task<bool> CanOpenAsync(string uri);
        Task<bool> CanOpenAsync(Uri uri);
        Task OpenAsync(string uri);
        Task OpenAsync(Uri uri);
        Task OpenAsync(OpenFileRequest request);
        Task<bool> TryOpenAsync(string uri);
        Task<bool> TryOpenAsync(Uri uri);
    }

    public interface IMagnetometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
        event EventHandler<MagnetometerChangedEventArgs> ReadingChanged;
    }

    [Obsolete]
    public interface IMainThread
    {
        [Obsolete]
        void BeginInvokeOnMainThread(Action action);

        [Obsolete]
        Task InvokeOnMainThreadAsync(Action action);

        [Obsolete]
        Task<T> InvokeOnMainThreadAsync<T>(Func<T> func);

        [Obsolete]
        Task InvokeOnMainThreadAsync(Func<Task> funcTask);

        [Obsolete]
        Task<T> InvokeOnMainThreadAsync<T>(Func<Task<T>> funcTask);

        [Obsolete]
        Task<SynchronizationContext> GetMainThreadSynchronizationContextAsync();

        [Obsolete] bool IsMainThread { get; }
    }

    public interface IMap
    {
        Task OpenAsync(Location location);
        Task OpenAsync(Location location, MapLaunchOptions options);
        Task OpenAsync(double latitude, double longitude);
        Task OpenAsync(double latitude, double longitude, MapLaunchOptions options);
        Task OpenAsync(Placemark placemark);
        Task OpenAsync(Placemark placemark, MapLaunchOptions options);
    }

    public interface IMediaPicker
    {
        Task<FileResult> PickPhotoAsync(MediaPickerOptions options = null);
        Task<FileResult> CapturePhotoAsync(MediaPickerOptions options = null);
        Task<FileResult> PickVideoAsync(MediaPickerOptions options = null);
        Task<FileResult> CaptureVideoAsync(MediaPickerOptions options = null);
        bool IsCaptureSupported { get; }
    }

    public interface IOrientationSensor
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
        event EventHandler<OrientationSensorChangedEventArgs> ReadingChanged;
    }

    public interface IPermissions
    {
        Task<PermissionStatus> CheckStatusAsync<TPermission>() where TPermission : Permissions.BasePermission, new();
        Task<PermissionStatus> RequestAsync<TPermission>() where TPermission : Permissions.BasePermission, new();
        bool ShouldShowRationale<TPermission>() where TPermission : Permissions.BasePermission, new();
    }

    public interface IPhoneDialer
    {
        void Open(string number);
    }

    public interface IPreferences
    {
        bool ContainsKey(string key);
        void Remove(string key);
        void Clear();
        string Get(string key, string defaultValue);
        bool Get(string key, bool defaultValue);
        int Get(string key, int defaultValue);
        double Get(string key, double defaultValue);
        float Get(string key, float defaultValue);
        long Get(string key, long defaultValue);
        void Set(string key, string value);
        void Set(string key, bool value);
        void Set(string key, int value);
        void Set(string key, double value);
        void Set(string key, float value);
        void Set(string key, long value);
        bool ContainsKey(string key, string sharedName);
        void Remove(string key, string sharedName);
        void Clear(string sharedName);
        string Get(string key, string defaultValue, string sharedName);
        bool Get(string key, bool defaultValue, string sharedName);
        int Get(string key, int defaultValue, string sharedName);
        double Get(string key, double defaultValue, string sharedName);
        float Get(string key, float defaultValue, string sharedName);
        long Get(string key, long defaultValue, string sharedName);
        void Set(string key, string value, string sharedName);
        void Set(string key, bool value, string sharedName);
        void Set(string key, int value, string sharedName);
        void Set(string key, double value, string sharedName);
        void Set(string key, float value, string sharedName);
        void Set(string key, long value, string sharedName);
        DateTime Get(string key, DateTime defaultValue);
        void Set(string key, DateTime value);
        DateTime Get(string key, DateTime defaultValue, string sharedName);
        void Set(string key, DateTime value, string sharedName);
    }

    public interface IScreenshot
    {
        Task<IScreenshotResult> CaptureAsync();
        bool IsCaptureSupported { get; }
    }

    public interface ISecureStorage
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value);
        bool Remove(string key);
        void RemoveAll();
    }

    public interface IShare
    {
        Task RequestAsync(string text);
        Task RequestAsync(string text, string title);
        Task RequestAsync(ShareTextRequest request);
        Task RequestAsync(ShareFileRequest request);
        Task RequestAsync(ShareMultipleFilesRequest request);
    }

    public interface ISms
    {
        Task ComposeAsync();
        Task ComposeAsync(SmsMessage message);
    }

    public interface ITextToSpeech
    {
        Task<IEnumerable<Locale>> GetLocalesAsync();
        Task SpeakAsync(string text, CancellationToken cancelToken = default);
        Task SpeakAsync(string text, SpeechOptions options, CancellationToken cancelToken = default);
    }

    public interface IVersionTracking
    {
        void Track();
        bool IsFirstLaunchForVersion(string version);
        bool IsFirstLaunchForBuild(string build);
        bool IsFirstLaunchEver { get; }
        bool IsFirstLaunchForCurrentVersion { get; }
        bool IsFirstLaunchForCurrentBuild { get; }
        string CurrentVersion { get; }
        string CurrentBuild { get; }
        string PreviousVersion { get; }
        string PreviousBuild { get; }
        string FirstInstalledVersion { get; }
        string FirstInstalledBuild { get; }
        IEnumerable<string> VersionHistory { get; }
        IEnumerable<string> BuildHistory { get; }
    }

    public interface IVibration
    {
        void Vibrate();
        void Vibrate(double duration);
        void Vibrate(TimeSpan duration);
        void Cancel();
    }

    public interface IWebAuthenticator
    {
        Task<WebAuthenticatorResult> AuthenticateAsync(Uri url, Uri callbackUrl);
        Task<WebAuthenticatorResult> AuthenticateAsync(WebAuthenticatorOptions webAuthenticatorOptions);
    }
}
