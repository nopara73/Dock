﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaDemo.CodeGen;
using AvaloniaDemo.Serializer;
using AvaloniaDemo.ViewModels;
using AvaloniaDemo.ViewModels.Tools;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Controls;

namespace AvaloniaDemo
{
    public class MainWindow : HostWindowBase
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();

            this.FindControl<MenuItem>("FileNew").Click += (sender, e) =>
            {
                if (this.DataContext is MainWindowViewModel vm)
                {
                    if (vm.Layout is IDock root)
                    {
                        root.HideWindows();
                        if (root.CurrentView is IDock dock)
                        {
                            dock.HideWindows();
                        }
                    }
                    vm.Factory = new EmptyDockFactory();
                    vm.Layout = vm.Factory.CreateLayout();
                    vm.Factory.InitLayout(vm.Layout, vm);
                }
            };

            this.FindControl<MenuItem>("FileOpen").Click += async (sender, e) =>
            {
                var dlg = new OpenFileDialog();
                dlg.Filters.Add(new FileDialogFilter() { Name = "Json", Extensions = { "json" } });
                dlg.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
                var result = await dlg.ShowAsync(this);
                if (result != null)
                {
                    if (this.DataContext is MainWindowViewModel vm)
                    {
                        IDock layout = ModelSerializer.Load<RootDock>(result.FirstOrDefault());
                        if (vm.Layout is IDock root)
                        {
                            root.HideWindows();
                            if (root.CurrentView is IDock dock)
                            {
                                dock.HideWindows();
                            }
                        }
                        vm.Layout = layout;
                        vm.Factory.InitLayout(vm.Layout, vm);
                    }
                }
            };

            this.FindControl<MenuItem>("FileSaveAs").Click += async (sender, e) =>
            {
                var dlg = new SaveFileDialog();
                dlg.Filters.Add(new FileDialogFilter() { Name = "Json", Extensions = { "json" } });
                dlg.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
                dlg.InitialFileName = "Layout";
                dlg.DefaultExtension = "json";
                var result = await dlg.ShowAsync(this);
                if (result != null)
                {
                    if (this.DataContext is MainWindowViewModel vm)
                    {
                        ModelSerializer.Save(result, vm.Layout);
                    }
                }
            };

            this.FindControl<MenuItem>("FileGenerateCode").Click += async (sender, e) =>
            {
                var dlg = new SaveFileDialog();
                dlg.Filters.Add(new FileDialogFilter() { Name = "C#", Extensions = { "cs" } });
                dlg.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
                dlg.InitialFileName = "ViewModel";
                dlg.DefaultExtension = "cs";
                var result = await dlg.ShowAsync(this);
                if (result != null)
                {
                    if (this.DataContext is MainWindowViewModel vm)
                    {
                        ICodeGen codeGeb = new CSharpCodeGen();
                        codeGeb.Generate(vm.Layout, result);
                    }
                }
            };

            this.FindControl<MenuItem>("ViewEditor").Click += (sender, e) =>
            {
                if (this.DataContext is MainWindowViewModel vm)
                {
                    var editorView = new EditorTool
                    {
                        Id = "Editor",
                        Width = double.NaN,
                        Height = double.NaN,
                        Title = "Editor"
                    };

                    var layout = new ToolDock
                    {
                        Id = nameof(IToolDock),
                        Dock = "",
                        Width = double.NaN,
                        Height = double.NaN,
                        Title = nameof(IToolDock),
                        CurrentView = editorView,
                        DefaultView = editorView,
                        Views = vm.Factory.CreateList<IView>
                        (
                            editorView
                        )
                    };

                    vm.Factory.Update(layout, null, vm.Layout);

                    var window = vm.Factory.CreateWindowFrom(layout);
                    if (window != null)
                    {
                        if (vm.Layout is IDock root)
                        {
                            vm.Factory.AddWindow(root, window, null);
                        }
                        window.X = 0;
                        window.Y = 0;
                        window.Width = 800;
                        window.Height = 600;
                        window.Present(false);
                    }
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
