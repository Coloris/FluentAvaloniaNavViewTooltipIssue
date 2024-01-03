using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using System.Collections.ObjectModel;

namespace FluentAvaloniaNavViewIssue.ViewModels
{

    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private object selectedCategory;

        [ObservableProperty]
        private ObservableCollection<CategoryBase> categories;

        public MainViewModel()
        {
            Categories = [];

            // App01
            var app01 = new Category
            {
                Name = "App01",
                Icon = Symbol.Globe,
                ToolTip = "App01 Category",
                Children =
                    [
                        new()
                        {
                            Name = "Sub01",
                            Icon = Symbol.Calendar,
                            ToolTip = "Sub01 Tooltip"
                        },
                        new()
                        {
                            Name = "Sub02",
                            Icon = Symbol.Bookmark,
                            ToolTip = "Sub02 Tooltip",
                            Children =
                            [
                                new() { Name = "Aaa", Icon = Symbol.Account, ToolTip = "Aaa Tooltip" },
                                new() { Name = "Bbb", Icon = Symbol.Audio, ToolTip = "Bbb Tooltip" },
                                new() { Name = "Ccc", Icon = Symbol.Calendar, ToolTip = "Ccc Tooltip" },
                            ]
                        }
                    ]
            };

            // App02
            var app02 = new Category
            {
                Name = "App02",
                Icon = Symbol.Globe,
                ToolTip = "App02 Category",
                Children =
                    [
                        new()
                        {
                            Name = "Sub01",
                            Icon = Symbol.Home,
                            ToolTip = "Sub01 Tooltip"
                        },
                        new()
                        {
                            Name = "Sub02",
                            Icon = Symbol.Home,
                            ToolTip = "Sub02 Tooltip",
                            Children =
                            [
                                new() { Name = "Aaa", Icon = Symbol.Account, ToolTip = "Aaa Tooltip" },
                                new() { Name = "Bbb", Icon = Symbol.Audio, ToolTip = "Bbb Tooltip" },
                                new() { Name = "Ccc", Icon = Symbol.Calendar, ToolTip = "Ccc Tooltip" }
                            ]
                        }
                    ]
            };

            //Build the navigation menu
            Categories.Add(new Category { Name = "Home", Icon = Symbol.Home, ToolTip = "Homepage" });
            Categories.Add(new Separator());
            Categories.Add(app01);
            Categories.Add(app02);

            //Default Category
            SelectedCategory = Categories[0];
        }
    }

    public abstract class CategoryBase : ObservableObject
    {

    }

    public partial class Category : CategoryBase
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private Symbol icon;

        [ObservableProperty]
        private string toolTip;

        [ObservableProperty]
        private ObservableCollection<Category> children;

    }

    public class Separator : CategoryBase
    {

    }

    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        [Content]
        public IDataTemplate ItemTemplate { get; set; }

        public IDataTemplate SeparatorTemplate { get; set; }

        protected override IDataTemplate SelectTemplateCore(object item)
        {
            return item is Separator ? SeparatorTemplate : ItemTemplate;
        }
    }
}
