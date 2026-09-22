// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.

using System.Windows;
using Wpf.Ui.Controls;

namespace TechBox.PluginContract
{
    /// <summary>
    /// Navigation menu item carrying TechBox's own per-page options, on top of everything
    /// <see cref="NavigationViewItem"/> already offers. Use it in place of
    /// <see cref="NavigationViewItem"/> when a menu entry needs one of them:
    /// <code>
    /// menu.MenuItems.Add(new TechBoxNavigationViewItem("SCCM", SymbolRegular.ClipboardTaskListLtr20, typeof(SCCMPage))
    /// {
    ///     AllowExternalWindow = true
    /// });
    /// </code>
    /// </summary>
    public class TechBoxNavigationViewItem : NavigationViewItem
    {
        /// <summary>Identifies the <see cref="AllowExternalWindow"/> dependency property.</summary>
        public static readonly DependencyProperty AllowExternalWindowProperty = DependencyProperty.Register(
            nameof(AllowExternalWindow),
            typeof(bool),
            typeof(TechBoxNavigationViewItem),
            new PropertyMetadata(false));

        public TechBoxNavigationViewItem()
        {
            // WPF-UI styles its navigation items through an implicit style, and an implicit style only
            // applies to its exact target type - a derived item would otherwise be left unstyled. This
            // asks for the very same style by its resource key, so the item looks like any other.
            SetResourceReference(StyleProperty, typeof(NavigationViewItem));
        }

        public TechBoxNavigationViewItem(Type targetPageType)
            : this()
        {
            TargetPageType = targetPageType;
        }

        public TechBoxNavigationViewItem(string content, Type targetPageType)
            : this(targetPageType)
        {
            Content = content;
        }

        public TechBoxNavigationViewItem(string content, SymbolRegular icon, Type targetPageType)
            : this(content, targetPageType)
        {
            Icon = new SymbolIcon { Symbol = icon };
        }

        /// <summary>
        /// Allows the page this item points to be opened in its own window, through the button at the
        /// right end of the breadcrumb line. <see langword="false"/> by default: the button stays
        /// hidden, and the page is only ever displayed inside the main window.
        /// </summary>
        /// <remarks>
        /// Only turn it on for pages that tolerate being displayed twice at once. The page and its
        /// view model must be registered with <c>AddScoped</c> for the detached window to get its own
        /// instance instead of sharing the main window's state.
        /// </remarks>
        public bool AllowExternalWindow
        {
            get => (bool)GetValue(AllowExternalWindowProperty);
            set => SetValue(AllowExternalWindowProperty, value);
        }
    }
}
