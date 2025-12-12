#if DEBUG
using System.Diagnostics;
using System.Text;
using Microsoft.Maui.Controls.Shapes;

namespace MauiStream.Helpers;

/// <summary>
/// Extension methods for debugging the visual tree of MAUI elements.
/// Outputs machine-parseable format with unique IDs for tooling integration.
/// </summary>
public static class VisualTreeDebugExtensions
{
    /// <summary>
    /// Prints the visual tree of the element to debug output.
    /// Output format is designed for tooling to parse and understand element relationships.
    /// </summary>
    /// <param name="element">The root element to print the tree from</param>
    /// <param name="label">Optional label to identify this tree dump (e.g., "StorePage.Loaded")</param>
    public static void PrintVisualTree(this IVisualTreeElement element, string? label = null)
    {
        var id = $"VT{DateTime.Now:yyyyMMddHHmmssfff}";
        var labelText = label ?? element.GetType().Name;

        Debug.WriteLine($"VT:ID={id}:START:{labelText}");

        PrintElement(element, id, "0");

        Debug.WriteLine($"VT:ID={id}:END");
    }

    private static void PrintElement(IVisualTreeElement element, string id, string path)
    {
        var properties = BuildPropertyString(element);
        var childCount = element.GetVisualChildren().Count;

        Debug.WriteLine($"VT:ID={id}:{path}:{element.GetType().Name}|{properties}|Children={childCount}");

        var children = element.GetVisualChildren();
        for (int i = 0; i < children.Count; i++)
        {
            var childPath = $"{path}.{i}";
            PrintElement(children[i], id, childPath);
        }
    }

    private static string BuildPropertyString(IVisualTreeElement element)
    {
        var sb = new StringBuilder();

        // Name / AutomationId
        var name = GetElementName(element);
        sb.Append($"Name={name}");

        // If it's a VisualElement, we can get layout properties
        if (element is VisualElement ve)
        {
            // Bounds
            sb.Append($"|Bounds={ve.Bounds.X:F0},{ve.Bounds.Y:F0},{ve.Bounds.Width:F0},{ve.Bounds.Height:F0}");

            // Visibility and state
            sb.Append($"|Visible={ve.IsVisible}");
            sb.Append($"|Enabled={ve.IsEnabled}");
            sb.Append($"|Opacity={ve.Opacity:F2}");
            sb.Append($"|InputTransparent={ve.InputTransparent}");

            // Margin (View has Margin, not VisualElement)
            if (element is View view)
            {
                sb.Append($"|Margin={view.Margin.Left:F0},{view.Margin.Top:F0},{view.Margin.Right:F0},{view.Margin.Bottom:F0}");
                sb.Append($"|HorizontalOptions={view.HorizontalOptions.Alignment}");
                sb.Append($"|VerticalOptions={view.VerticalOptions.Alignment}");
            }

            // Grid attachment properties (if parent is Grid)
            if (ve.Parent is Grid)
            {
                sb.Append($"|Row={Grid.GetRow(ve)}");
                sb.Append($"|Col={Grid.GetColumn(ve)}");
                var rowSpan = Grid.GetRowSpan(ve);
                var colSpan = Grid.GetColumnSpan(ve);
                if (rowSpan > 1) sb.Append($"|RowSpan={rowSpan}");
                if (colSpan > 1) sb.Append($"|ColSpan={colSpan}");
            }
        }

        // Padding (for Layout types)
        if (element is Layout layout)
        {
            sb.Append($"|Padding={layout.Padding.Left:F0},{layout.Padding.Top:F0},{layout.Padding.Right:F0},{layout.Padding.Bottom:F0}");
        }

        // Content-specific properties
        AppendContentProperties(element, sb);

        // BindingContext type
        if (element is BindableObject bo && bo.BindingContext != null)
        {
            sb.Append($"|Context={bo.BindingContext.GetType().Name}");
        }

        return sb.ToString();
    }

    private static string GetElementName(IVisualTreeElement element)
    {
        if (element is Element e)
        {
            // Try AutomationId first, then StyleId (which holds x:Name in some cases)
            if (!string.IsNullOrEmpty(e.AutomationId))
                return e.AutomationId;
            if (!string.IsNullOrEmpty(e.StyleId))
                return e.StyleId;
        }
        return string.Empty;
    }

    private static void AppendContentProperties(IVisualTreeElement element, StringBuilder sb)
    {
        switch (element)
        {
            case Label label:
                var text = TruncateText(label.Text, 50);
                sb.Append($"|Text=\"{EscapeText(text)}\"");
                sb.Append($"|FontSize={label.FontSize:F0}");
                break;

            case Button button:
                var btnText = TruncateText(button.Text, 30);
                sb.Append($"|Text=\"{EscapeText(btnText)}\"");
                break;

            case Entry entry:
                var entryText = TruncateText(entry.Text, 30);
                sb.Append($"|Text=\"{EscapeText(entryText)}\"");
                sb.Append($"|Placeholder=\"{EscapeText(entry.Placeholder)}\"");
                break;

            case Editor editor:
                var editorText = TruncateText(editor.Text, 30);
                sb.Append($"|Text=\"{EscapeText(editorText)}\"");
                break;

            case Image image:
                var source = image.Source?.ToString() ?? string.Empty;
                sb.Append($"|Source={source}");
                break;

            case ImageButton imageButton:
                var imgBtnSource = imageButton.Source?.ToString() ?? string.Empty;
                sb.Append($"|Source={imgBtnSource}");
                break;

            case CollectionView cv:
                var itemsCount = (cv.ItemsSource as System.Collections.ICollection)?.Count ?? 0;
                sb.Append($"|ItemsCount={itemsCount}");
                break;

            case CarouselView carousel:
                var carouselCount = (carousel.ItemsSource as System.Collections.ICollection)?.Count ?? 0;
                sb.Append($"|ItemsCount={carouselCount}");
                sb.Append($"|Position={carousel.Position}");
                break;

            case ScrollView scrollView:
                sb.Append($"|ScrollX={scrollView.ScrollX:F0}");
                sb.Append($"|ScrollY={scrollView.ScrollY:F0}");
                break;

            case ContentPage page:
                sb.Append($"|Title=\"{EscapeText(page.Title)}\"");
                break;

            case Frame frame:
                sb.Append($"|CornerRadius={frame.CornerRadius}");
                sb.Append($"|HasShadow={frame.HasShadow}");
                break;

            case Border border:
                if (border.StrokeShape is Microsoft.Maui.Controls.Shapes.RoundRectangle rr)
                {
                    sb.Append($"|CornerRadius={rr.CornerRadius}");
                }
                break;

            case BoxView boxView:
                sb.Append($"|Color={boxView.Color}");
                break;

            case ActivityIndicator ai:
                sb.Append($"|IsRunning={ai.IsRunning}");
                break;

            case ProgressBar pb:
                sb.Append($"|Progress={pb.Progress:F2}");
                break;

            case Microsoft.Maui.Controls.Switch sw:
                sb.Append($"|IsToggled={sw.IsToggled}");
                break;

            case CheckBox cb:
                sb.Append($"|IsChecked={cb.IsChecked}");
                break;

            case Slider slider:
                sb.Append($"|Value={slider.Value:F2}");
                sb.Append($"|Min={slider.Minimum:F0}");
                sb.Append($"|Max={slider.Maximum:F0}");
                break;

            case Picker picker:
                sb.Append($"|SelectedIndex={picker.SelectedIndex}");
                sb.Append($"|ItemsCount={picker.Items.Count}");
                break;

            case DatePicker datePicker:
                sb.Append($"|Date={datePicker.Date:yyyy-MM-dd}");
                break;

            case TimePicker timePicker:
                sb.Append($"|Time={timePicker.Time}");
                break;
        }
    }

    private static string TruncateText(string? text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (text.Length <= maxLength) return text;
        return text.Substring(0, maxLength) + "...";
    }

    private static string EscapeText(string? text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Replace("\\", "\\\\").Replace("|", "\\|").Replace("\n", "\\n").Replace("\r", "\\r");
    }
}
#endif
