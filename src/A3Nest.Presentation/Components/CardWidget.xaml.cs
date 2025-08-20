using System.Windows.Input;

namespace A3Nest.Presentation.Components;

public partial class CardWidget : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(CardWidget),
            string.Empty);

    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(
            nameof(IconSource),
            typeof(ImageSource),
            typeof(CardWidget),
            null,
            propertyChanged: OnIconSourceChanged);

    public static readonly BindableProperty PrimaryValueProperty =
        BindableProperty.Create(
            nameof(PrimaryValue),
            typeof(string),
            typeof(CardWidget),
            string.Empty,
            propertyChanged: OnPrimaryValueChanged);

    public static readonly BindableProperty PrimaryValueColorProperty =
        BindableProperty.Create(
            nameof(PrimaryValueColor),
            typeof(Color),
            typeof(CardWidget),
            Colors.Black);

    public static readonly BindableProperty SecondaryValueProperty =
        BindableProperty.Create(
            nameof(SecondaryValue),
            typeof(string),
            typeof(CardWidget),
            string.Empty,
            propertyChanged: OnSecondaryValueChanged);

    public static readonly BindableProperty CustomContentProperty =
        BindableProperty.Create(
            nameof(CustomContent),
            typeof(View),
            typeof(CardWidget),
            null);

    public static readonly BindableProperty FooterTextProperty =
        BindableProperty.Create(
            nameof(FooterText),
            typeof(string),
            typeof(CardWidget),
            string.Empty,
            propertyChanged: OnFooterTextChanged);

    public static readonly BindableProperty ActionButtonTextProperty =
        BindableProperty.Create(
            nameof(ActionButtonText),
            typeof(string),
            typeof(CardWidget),
            string.Empty,
            propertyChanged: OnActionButtonTextChanged);

    public static readonly BindableProperty ActionCommandProperty =
        BindableProperty.Create(
            nameof(ActionCommand),
            typeof(ICommand),
            typeof(CardWidget),
            null,
            propertyChanged: OnActionCommandChanged);

    // Computed properties for visibility
    public static readonly BindableProperty HasIconProperty =
        BindableProperty.Create(
            nameof(HasIcon),
            typeof(bool),
            typeof(CardWidget),
            false);

    public static readonly BindableProperty HasPrimaryValueProperty =
        BindableProperty.Create(
            nameof(HasPrimaryValue),
            typeof(bool),
            typeof(CardWidget),
            false);

    public static readonly BindableProperty HasSecondaryValueProperty =
        BindableProperty.Create(
            nameof(HasSecondaryValue),
            typeof(bool),
            typeof(CardWidget),
            false);

    public static readonly BindableProperty HasFooterProperty =
        BindableProperty.Create(
            nameof(HasFooter),
            typeof(bool),
            typeof(CardWidget),
            false);

    public static readonly BindableProperty HasActionButtonProperty =
        BindableProperty.Create(
            nameof(HasActionButton),
            typeof(bool),
            typeof(CardWidget),
            false);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string PrimaryValue
    {
        get => (string)GetValue(PrimaryValueProperty);
        set => SetValue(PrimaryValueProperty, value);
    }

    public Color PrimaryValueColor
    {
        get => (Color)GetValue(PrimaryValueColorProperty);
        set => SetValue(PrimaryValueColorProperty, value);
    }

    public string SecondaryValue
    {
        get => (string)GetValue(SecondaryValueProperty);
        set => SetValue(SecondaryValueProperty, value);
    }

    public View CustomContent
    {
        get => (View)GetValue(CustomContentProperty);
        set => SetValue(CustomContentProperty, value);
    }

    public string FooterText
    {
        get => (string)GetValue(FooterTextProperty);
        set => SetValue(FooterTextProperty, value);
    }

    public string ActionButtonText
    {
        get => (string)GetValue(ActionButtonTextProperty);
        set => SetValue(ActionButtonTextProperty, value);
    }

    public ICommand ActionCommand
    {
        get => (ICommand)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }

    public bool HasIcon
    {
        get => (bool)GetValue(HasIconProperty);
        private set => SetValue(HasIconProperty, value);
    }

    public bool HasPrimaryValue
    {
        get => (bool)GetValue(HasPrimaryValueProperty);
        private set => SetValue(HasPrimaryValueProperty, value);
    }

    public bool HasSecondaryValue
    {
        get => (bool)GetValue(HasSecondaryValueProperty);
        private set => SetValue(HasSecondaryValueProperty, value);
    }

    public bool HasFooter
    {
        get => (bool)GetValue(HasFooterProperty);
        private set => SetValue(HasFooterProperty, value);
    }

    public bool HasActionButton
    {
        get => (bool)GetValue(HasActionButtonProperty);
        private set => SetValue(HasActionButtonProperty, value);
    }

    public CardWidget()
    {
        InitializeComponent();
    }

    private static void OnIconSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CardWidget cardWidget)
        {
            cardWidget.HasIcon = newValue != null;
        }
    }

    private static void OnPrimaryValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CardWidget cardWidget)
        {
            cardWidget.HasPrimaryValue = !string.IsNullOrEmpty(newValue?.ToString());
        }
    }

    private static void OnSecondaryValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CardWidget cardWidget)
        {
            cardWidget.HasSecondaryValue = !string.IsNullOrEmpty(newValue?.ToString());
        }
    }

    private static void OnFooterTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CardWidget cardWidget)
        {
            cardWidget.UpdateFooterVisibility();
        }
    }

    private static void OnActionButtonTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CardWidget cardWidget)
        {
            cardWidget.HasActionButton = !string.IsNullOrEmpty(newValue?.ToString());
            cardWidget.UpdateFooterVisibility();
        }
    }

    private static void OnActionCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CardWidget cardWidget)
        {
            cardWidget.UpdateFooterVisibility();
        }
    }

    private void UpdateFooterVisibility()
    {
        HasFooter = !string.IsNullOrEmpty(FooterText) || HasActionButton;
    }
}