// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.

namespace TechBox.PluginContract
{
    /// <summary>
    /// Implemented by a page view model that carries its state over when the page is opened in its
    /// own window, so the detached window opens on whatever the main window was displaying instead
    /// of on an empty page.
    /// </summary>
    /// <remarks>
    /// Only worth implementing on a page whose menu entry sets
    /// <see cref="TechBoxNavigationViewItem.AllowExternalWindow"/>. Without it, the detached window
    /// still opens - the page simply starts empty, since a scoped view model is a new instance.
    /// </remarks>
    public interface IExternalWindowState
    {
        /// <summary>
        /// Copies onto this view model what <paramref name="source"/> - the view model of the same
        /// page, as displayed in the main window - is currently showing. Called on the detached
        /// window's view model once the page's usual navigation life cycle has run, so the lists it
        /// loads there are already in place.
        /// </summary>
        /// <param name="source">
        /// View model being copied from. Always test its type: it is the object the source page
        /// exposes, and nothing guarantees it is the type expected.
        /// </param>
        Task CopyStateFromAsync(object source);
    }
}
