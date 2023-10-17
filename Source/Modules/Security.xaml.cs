using System;
using System.Linq;
using System.Windows;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using HomeControl.Source.ViewModel;
using Panel = Windows.Devices.Enumeration.Panel;
using RoutedEventArgs = Windows.UI.Xaml.RoutedEventArgs;

namespace HomeControl.Source.Modules;

public partial class Security : Window {
    private readonly CaptureElement _captureElement;
    private readonly MediaCapture _mediaCapture = new();
    private StorageFolder _captureFolder;
    private bool _initialized;
    private bool _isRecording;

    public Security() {
        InitializeComponent();
        DataContext = new SecurityVM();

        _captureElement = new CaptureElement {
            Stretch = Stretch.Uniform
        };
        _captureElement.Loaded += CaptureElement_Loaded;
        _captureElement.Unloaded += CaptureElement_Unloaded;

        XamlHost.Child = _captureElement;
    }

    private async void CaptureElement_Unloaded(object sender, RoutedEventArgs e) {
        await _mediaCapture.StopPreviewAsync();
    }

    private async void CaptureElement_Loaded(object sender, RoutedEventArgs e) {
        if (!_initialized) {
            StorageLibrary picturesLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            // Fall back to the local app storage if the Pictures Library is not available
            _captureFolder = picturesLibrary.SaveFolder ?? ApplicationData.Current.LocalFolder;

            // Get available devices for capturing pictures
            DeviceInformationCollection allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            if (allVideoDevices.Count > 0) {
                // try to find back camera
                DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == Panel.Back);

                // If there is no device mounted on the back panel, return the first device found
                DeviceInformation device = desiredDevice ?? allVideoDevices.FirstOrDefault();

                await _mediaCapture.InitializeAsync(new MediaCaptureInitializationSettings { VideoDeviceId = device.Id });
                _captureElement.Source = _mediaCapture;

                _initialized = true;
            }
        }

        if (_initialized) {
            await _mediaCapture.StartPreviewAsync();
        }
    }

    private async void Video_Click(object sender, System.Windows.RoutedEventArgs e) {
        if (!_initialized) {
            return;
        }

        if (_isRecording) {
            // stop recording
            _isRecording = false;
            await _mediaCapture.StopRecordAsync();
        } else {
            // start recording
            StorageFile videoFile = await _captureFolder.CreateFileAsync("Video.wmv", CreationCollisionOption.GenerateUniqueName);

            MediaEncodingProfile encodingProfile = MediaEncodingProfile.CreateWmv(VideoEncodingQuality.Auto);

            await _mediaCapture.StartRecordToStorageFileAsync(encodingProfile, videoFile);

            _isRecording = true;
        }
    }
}