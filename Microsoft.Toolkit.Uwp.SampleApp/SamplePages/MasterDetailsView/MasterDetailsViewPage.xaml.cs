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
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.SampleApp.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Toolkit.Uwp.SampleApp.SamplePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterDetailsViewPage : Page, IXamlRenderListener
    {
        public MasterDetailsViewPage()
        {
            Emails = new List<Email>
            {
                new Email
                {
                    From = "Steve Johnson",
                    Subject = "Lunch Tomorrow",
                    Body = "Are you available for lunch tomorrow? A client would like to discuss a project with you.",
                    Thumbnail = new Uri("ms-appx:///Assets/People/shen.png")
                },
                new Email
                {
                    From = "Pete Davidson",
                    Subject = "Kids game",
                    Body = "Don't forget the kids have their soccer game this Friday. We have to supply end of game snacks.",
                    Thumbnail = new Uri("ms-appx:///Assets/People/pete.png")
                },
                new Email
                {
                    From = "OneDrive",
                    Subject = "Check out your event recap",
                    Body = "Your new album.\r\nYou uploaded some photos to yuor OneDrive and automatically created an album for you.",
                    Thumbnail = new Uri("ms-appx:///SamplePages/MasterDetailsView/OneDriveLogo.png")
                },
                new Email
                {
                    From = "Twitter",
                    Subject = "Follow randomPerson, APersonYouMightKnow",
                    Body = "Here are some people we think you might like to follow:\r\n.@randomPerson\r\nAPersonYouMightKnow",
                    Thumbnail = new Uri("ms-appx:///SamplePages/Twitter Service/icon.png")
                },
            };

            InitializeComponent();
        }

        public ICollection<Email> Emails { get; set; }

        public void OnXamlRendered(FrameworkElement control)
        {
            // Transfer Data Context so we can access Emails Collection.
            control.DataContext = this;
        }
    }
}
