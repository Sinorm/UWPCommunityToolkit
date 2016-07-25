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
using Microsoft.Windows.Toolkit.Notifications.Adaptive.Elements;

namespace Microsoft.Windows.Toolkit.Notifications
{
    /// <summary>
    /// An inline image.
    /// </summary>
    public sealed class AdaptiveImage
        : IBaseImage,
        IToastBindingGenericChild,
        ITileBindingContentAdaptiveChild,
        IAdaptiveChild,
        IAdaptiveSubgroupChild
    {
#if ANNIVERSARY_UPDATE
        /// <summary>
        /// Control the desired cropping of the image.
        /// </summary>
#else
        /// <summary>
        /// Control the desired cropping of the image. Not supported on Toast.
        /// </summary>
#endif
        public AdaptiveImageCrop HintCrop { get; set; }

#if ANNIVERSARY_UPDATE
        /// <summary>
        /// By default, images have an 8px margin around them. You can remove this margin by setting this property to true.
        /// </summary>
#else
        /// <summary>
        /// By default, images have an 8px margin around them. You can remove this margin by setting this property to true. Not supported on Toast.
        /// </summary>
#endif
        public bool? HintRemoveMargin { get; set; }

#if ANNIVERSARY_UPDATE
        /// <summary>
        /// The horizontal alignment of the image. For Toast, this is only supported when inside an <see cref="AdaptiveSubgroup"/>.
        /// </summary>
#else
        /// <summary>
        /// The horizontal alignment of the image. Not supported on Toast.
        /// </summary>
#endif
        public AdaptiveImageAlign HintAlign { get; set; }

        private string _source;

        /// <summary>
        /// Required. The URI of the image. Can be from your application package, application data, or the internet. Internet images must be less than 200 KB in size.
        /// </summary>
        public string Source
        {
            get { return _source; }
            set { BaseImageHelper.SetSource(ref _source, value); }
        }

        /// <summary>
        /// A description of the image, for users of assistive technologies.
        /// </summary>
        public string AlternateText { get; set; }

        /// <summary>
        /// Set to true to allow Windows to append a query string to the image URI supplied in the Tile notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language.
        /// </summary>
        public bool? AddImageQuery { get; set; }

        /// <summary>
        /// Returns the image's source string.
        /// </summary>
        /// <returns>The image's source string.</returns>
        public override string ToString()
        {
            if (Source == null)
            {
                return "Source is null";
            }

            return Source;
        }

        internal Element_AdaptiveImage ConvertToElement()
        {
            Element_AdaptiveImage image = BaseImageHelper.CreateBaseElement(this);

            image.Crop = HintCrop;
            image.RemoveMargin = HintRemoveMargin;
            image.Align = HintAlign;
            image.Placement = AdaptiveImagePlacement.Inline;

            return image;
        }
    }
}