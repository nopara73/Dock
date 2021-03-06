﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaDemo.Controls
{
    /// <summary>
    /// Interaction logic for <see cref="DockWindowPropertiesControl"/> xaml.
    /// </summary>
    public class DockWindowPropertiesControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DockWindowPropertiesControl"/> class.
        /// </summary>
        public DockWindowPropertiesControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initialize the Xaml components.
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
