﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Microsoft.Toolkit.Uwp.SampleApp.SamplePages
{
    public sealed partial class InAppNotificationPage : Page, IXamlRenderListener
    {
        private ControlTemplate _defaultInAppNotificationControlTemplate;
        private InAppNotification _exampleInAppNotification;
        private InAppNotification _exampleVSCodeInAppNotification;
        private ResourceDictionary _resources;

        public bool IsRootGridActualWidthLargerThan700 { get; set; }

        public int NotificationDuration { get; set; } = 0;

        public InAppNotificationPage()
        {
            InitializeComponent();
        }

        public void OnXamlRendered(FrameworkElement control)
        {
            NotificationDuration = 0;

            _exampleInAppNotification = control.FindChildByName("ExampleInAppNotification") as InAppNotification;
            _defaultInAppNotificationControlTemplate = _exampleInAppNotification?.Template;
            _exampleVSCodeInAppNotification = control.FindChildByName("ExampleVSCodeInAppNotification") as InAppNotification;
            _resources = control.Resources;

            var notificationDurationTextBox = control.FindChildByName("NotificationDurationTextBox") as TextBox;
            if (notificationDurationTextBox != null)
            {
                notificationDurationTextBox.TextChanged += NotificationDurationTextBox_TextChanged;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Shell.Current.RegisterNewCommand("Show notification with random text", async (sender, args) =>
            {
                _exampleVSCodeInAppNotification?.Dismiss();
                SetDefaultControlTemplate();

                var random = new Random();
                int result = random.Next(1, 4);

                if (result == 1)
                {
                    await _exampleInAppNotification?.ShowAsync("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sollicitudin bibendum enim at tincidunt. Praesent egestas ipsum ligula, nec tincidunt lacus semper non.", NotificationDuration);
                }

                if (result == 2)
                {
                    await _exampleInAppNotification?.ShowAsync("Pellentesque in risus eget leo rhoncus ultricies nec id ante.", NotificationDuration);
                }

                if (result == 3)
                {
                    await _exampleInAppNotification?.ShowAsync("Sed quis nisi quis nunc condimentum varius id consectetur metus. Duis mauris sapien, commodo eget erat ac, efficitur iaculis magna. Morbi eu velit nec massa pharetra cursus. Fusce non quam egestas leo finibus interdum eu ac massa. Quisque nec justo leo. Aenean scelerisque placerat ultrices. Sed accumsan lorem at arcu commodo tristique.", NotificationDuration);
                }
            });

            Shell.Current.RegisterNewCommand("Show notification with buttons (without DataTemplate)", async (sender, args) =>
            {
                _exampleVSCodeInAppNotification?.Dismiss();
                SetDefaultControlTemplate();

                var grid = new Grid();

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                // Text part
                var textBlock = new TextBlock
                {
                    Text = "Do you like it?",
                    VerticalAlignment = VerticalAlignment.Center
                };
                grid.Children.Add(textBlock);

                // Buttons part
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var yesButton = new Button
                {
                    Content = "Yes",
                    Width = 150,
                    Height = 30
                };
                yesButton.Click += YesButton_Click;
                stackPanel.Children.Add(yesButton);

                var noButton = new Button
                {
                    Content = "No",
                    Width = 150,
                    Height = 30,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                noButton.Click += NoButton_Click;
                stackPanel.Children.Add(noButton);

                Grid.SetColumn(stackPanel, 1);
                grid.Children.Add(stackPanel);

                await _exampleInAppNotification?.ShowAsync(grid, NotificationDuration);
            });

            Shell.Current.RegisterNewCommand("Show notification with buttons (with DataTemplate)", async (sender, args) =>
            {
                _exampleVSCodeInAppNotification?.Dismiss();
                SetDefaultControlTemplate();

                object inAppNotificationWithButtonsTemplate = null;
                bool? isTemplatePresent = _resources?.TryGetValue("InAppNotificationWithButtonsTemplate", out inAppNotificationWithButtonsTemplate);

                if (isTemplatePresent == true && inAppNotificationWithButtonsTemplate is DataTemplate template)
                {
                    await _exampleInAppNotification.ShowAsync(template, NotificationDuration);
                }
            });

            Shell.Current.RegisterNewCommand("Show notification with Drop Shadow (based on default template)", async (sender, args) =>
            {
                _exampleVSCodeInAppNotification.Dismiss();

                // Update control template
                object inAppNotificationDropShadowControlTemplate = null;
                bool? isTemplatePresent = _resources?.TryGetValue("InAppNotificationDropShadowControlTemplate", out inAppNotificationDropShadowControlTemplate);

                if (isTemplatePresent == true && inAppNotificationDropShadowControlTemplate is ControlTemplate template)
                {
                    _exampleInAppNotification.Template = template;
                }

                await _exampleInAppNotification.ShowAsync(NotificationDuration);
            });

            Shell.Current.RegisterNewCommand("Show notification with Visual Studio Code template (info notification)", async (sender, args) =>
            {
                _exampleInAppNotification.Dismiss();
                await _exampleVSCodeInAppNotification.ShowAsync(NotificationDuration);
            });

            Shell.Current.RegisterNewCommand("Dismiss", (sender, args) =>
            {
                // Dismiss all notifications (should not be replicated in production)
                _exampleInAppNotification.Dismiss();
                _exampleVSCodeInAppNotification.Dismiss();
            });
        }

        private void SetDefaultControlTemplate()
        {
            // Update control template
            _exampleInAppNotification.Template = _defaultInAppNotificationControlTemplate;
        }

        private void NotificationDurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int newDuration;
            if (int.TryParse((sender as TextBox)?.Text, out newDuration))
            {
                NotificationDuration = newDuration;
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _exampleInAppNotification?.Dismiss();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _exampleInAppNotification?.Dismiss();
        }
    }

#pragma warning disable SA1402 // File may only contain a single class
    internal class DismissCommand : ICommand
#pragma warning restore SA1402 // File may only contain a single class
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
            }

            remove
            {
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as InAppNotification)?.Dismiss();
        }
    }
}
