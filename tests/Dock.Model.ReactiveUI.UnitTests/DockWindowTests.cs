﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Xunit;

namespace Dock.Model.ReactiveUI.UnitTests
{
    public class DockWindowTests
    {
        [Fact]
        public void DockWindow_Ctor()
        {
            var actual = new DockWindow();
            Assert.NotNull(actual);
        }
    }
}
