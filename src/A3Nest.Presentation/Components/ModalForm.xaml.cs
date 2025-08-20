using System.Collections.ObjectModel;
using System.Windows.Input;

namespace A3Nest.Presentation.Components;

public partial class ModalForm : ContentView
{
    public static readonly new BindableProperty IsVisibleProperty =
        BindableProperty.Create(
            nameof(IsVisible),
            typeof(bool),
            typeof(ModalForm),
            false);

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ModalForm),
            "Form");

    public static readonly BindableProperty FormFieldsProperty =
        BindableProperty.Create(
            nameof(FormFields),
            typeof(ObservableCollection<FormField>),
            typeof(ModalForm),
            null);

    public static readonly BindableProperty CustomContentProperty =
        BindableProperty.Create(
            nameof(CustomContent),
            typeof(View),
            typeof(ModalForm),
            null);

    public static readonly BindableProperty SubmitCommandProperty =
        BindableProperty.Create(
            nameof(SubmitCommand),
            typeof(ICommand),
            typeof(ModalForm),
            null);

    public static readonly BindableProperty CancelCommandProperty =
        BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(ModalForm),
            null);

    public static readonly BindableProperty CloseCommandProperty =
        BindableProperty.Create(
            nameof(CloseCommand),
            typeof(ICommand),
            typeof(ModalForm),
            null);

    public static readonly BindableProperty SubmitButtonTextProperty =
        BindableProperty.Create(
            nameof(SubmitButtonText),
            typeof(string),
            typeof(ModalForm),
            "Submit");

    public static readonly BindableProperty CancelButtonTextProperty =
        BindableProperty.Create(
            nameof(CancelButtonText),
            typeof(string),
            typeof(ModalForm),
            "Cancel");

    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(ModalForm),
            false,
            propertyChanged: OnIsLoadingChanged);

    public static readonly BindableProperty ModalWidthProperty =
        BindableProperty.Create(
            nameof(ModalWidth),
            typeof(double),
            typeof(ModalForm),
            400.0);

    public static readonly BindableProperty ModalHeightProperty =
        BindableProperty.Create(
            nameof(ModalHeight),
            typeof(double),
            typeof(ModalForm),
            500.0);

    // Computed properties
    public static readonly BindableProperty IsNotLoadingProperty =
        BindableProperty.Create(
            nameof(IsNotLoading),
            typeof(bool),
            typeof(ModalForm),
            true);

    public static readonly BindableProperty CanSubmitProperty =
        BindableProperty.Create(
            nameof(CanSubmit),
            typeof(bool),
            typeof(ModalForm),
            true);

    public new bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public ObservableCollection<FormField> FormFields
    {
        get => (ObservableCollection<FormField>)GetValue(FormFieldsProperty);
        set => SetValue(FormFieldsProperty, value);
    }

    public View CustomContent
    {
        get => (View)GetValue(CustomContentProperty);
        set => SetValue(CustomContentProperty, value);
    }

    public ICommand SubmitCommand
    {
        get => (ICommand)GetValue(SubmitCommandProperty);
        set => SetValue(SubmitCommandProperty, value);
    }

    public ICommand CancelCommand
    {
        get => (ICommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public ICommand CloseCommand
    {
        get => (ICommand)GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }

    public string SubmitButtonText
    {
        get => (string)GetValue(SubmitButtonTextProperty);
        set => SetValue(SubmitButtonTextProperty, value);
    }

    public string CancelButtonText
    {
        get => (string)GetValue(CancelButtonTextProperty);
        set => SetValue(CancelButtonTextProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public double ModalWidth
    {
        get => (double)GetValue(ModalWidthProperty);
        set => SetValue(ModalWidthProperty, value);
    }

    public double ModalHeight
    {
        get => (double)GetValue(ModalHeightProperty);
        set => SetValue(ModalHeightProperty, value);
    }

    public bool IsNotLoading
    {
        get => (bool)GetValue(IsNotLoadingProperty);
        private set => SetValue(IsNotLoadingProperty, value);
    }

    public bool CanSubmit
    {
        get => (bool)GetValue(CanSubmitProperty);
        set => SetValue(CanSubmitProperty, value);
    }

    public ModalForm()
    {
        InitializeComponent();
        FormFields = new ObservableCollection<FormField>();
        
        // Set up default commands
        CloseCommand = new Command(OnCloseCommand);
        CancelCommand = new Command(OnCancelCommand);
        
        // Subscribe to form field changes for validation
        FormFields.CollectionChanged += OnFormFieldsChanged;
    }

    private static void OnIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ModalForm modalForm)
        {
            modalForm.IsNotLoading = !(bool)newValue;
            modalForm.UpdateCanSubmit();
        }
    }

    private void OnFormFieldsChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (FormField field in e.NewItems)
            {
                field.PropertyChanged += OnFormFieldPropertyChanged;
            }
        }

        if (e.OldItems != null)
        {
            foreach (FormField field in e.OldItems)
            {
                field.PropertyChanged -= OnFormFieldPropertyChanged;
            }
        }

        UpdateCanSubmit();
    }

    private void OnFormFieldPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(FormField.IsValid))
        {
            UpdateCanSubmit();
        }
    }

    private void UpdateCanSubmit()
    {
        CanSubmit = !IsLoading && FormFields.All(f => f.IsValid);
    }

    private void OnCloseCommand()
    {
        IsVisible = false;
    }

    private void OnCancelCommand()
    {
        IsVisible = false;
    }

    // Public methods for form management
    public void Show()
    {
        IsVisible = true;
    }

    public void Hide()
    {
        IsVisible = false;
    }

    public void AddField(FormField field)
    {
        FormFields.Add(field);
    }

    public void ClearFields()
    {
        FormFields.Clear();
    }

    public Dictionary<string, object> GetFormData()
    {
        var data = new Dictionary<string, object>();
        foreach (var field in FormFields)
        {
            data[field.Name] = field.GetValue();
        }
        return data;
    }

    public void SetFormData(Dictionary<string, object> data)
    {
        foreach (var field in FormFields)
        {
            if (data.ContainsKey(field.Name))
            {
                field.SetValue(data[field.Name]);
            }
        }
    }

    public bool ValidateForm()
    {
        bool isValid = true;
        foreach (var field in FormFields)
        {
            if (!field.Validate())
            {
                isValid = false;
            }
        }
        return isValid;
    }
}

public class FormField : BindableObject
{
    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(FormField), string.Empty);

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(FormField), string.Empty);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(string), typeof(FormField), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(FormField), string.Empty);

    public static readonly BindableProperty FieldTypeProperty =
        BindableProperty.Create(nameof(FieldType), typeof(FormFieldType), typeof(FormField), FormFieldType.Entry);

    public static readonly BindableProperty IsRequiredProperty =
        BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(FormField), false);

    public static readonly BindableProperty IsEnabledProperty =
        BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(FormField), true);

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(FormField), false);

    public static readonly BindableProperty KeyboardTypeProperty =
        BindableProperty.Create(nameof(KeyboardType), typeof(Keyboard), typeof(FormField), Keyboard.Default);

    public static readonly BindableProperty ValidationErrorProperty =
        BindableProperty.Create(nameof(ValidationError), typeof(string), typeof(FormField), string.Empty);

    public static readonly BindableProperty PickerItemsProperty =
        BindableProperty.Create(nameof(PickerItems), typeof(IList<string>), typeof(FormField), null);

    public static readonly BindableProperty DateValueProperty =
        BindableProperty.Create(nameof(DateValue), typeof(DateTime), typeof(FormField), DateTime.Today, BindingMode.TwoWay);

    public static readonly BindableProperty BoolValueProperty =
        BindableProperty.Create(nameof(BoolValue), typeof(bool), typeof(FormField), false, BindingMode.TwoWay);

    // Computed properties for field type visibility
    public bool IsEntry => FieldType == FormFieldType.Entry;
    public bool IsEditor => FieldType == FormFieldType.Editor;
    public bool IsPicker => FieldType == FormFieldType.Picker;
    public bool IsDatePicker => FieldType == FormFieldType.DatePicker;
    public bool IsSwitch => FieldType == FormFieldType.Switch;
    public bool HasValidationError => !string.IsNullOrEmpty(ValidationError);
    public bool IsValid => string.IsNullOrEmpty(ValidationError);

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public FormFieldType FieldType
    {
        get => (FormFieldType)GetValue(FieldTypeProperty);
        set => SetValue(FieldTypeProperty, value);
    }

    public bool IsRequired
    {
        get => (bool)GetValue(IsRequiredProperty);
        set => SetValue(IsRequiredProperty, value);
    }

    public bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public Keyboard KeyboardType
    {
        get => (Keyboard)GetValue(KeyboardTypeProperty);
        set => SetValue(KeyboardTypeProperty, value);
    }

    public string ValidationError
    {
        get => (string)GetValue(ValidationErrorProperty);
        set => SetValue(ValidationErrorProperty, value);
    }

    public IList<string> PickerItems
    {
        get => (IList<string>)GetValue(PickerItemsProperty);
        set => SetValue(PickerItemsProperty, value);
    }

    public DateTime DateValue
    {
        get => (DateTime)GetValue(DateValueProperty);
        set => SetValue(DateValueProperty, value);
    }

    public bool BoolValue
    {
        get => (bool)GetValue(BoolValueProperty);
        set => SetValue(BoolValueProperty, value);
    }

    public Func<string, string>? ValidationRule { get; set; }

    public bool Validate()
    {
        ValidationError = string.Empty;

        if (IsRequired && string.IsNullOrWhiteSpace(Value))
        {
            ValidationError = $"{Label} is required.";
            return false;
        }

        if (ValidationRule != null && !string.IsNullOrEmpty(Value))
        {
            ValidationError = ValidationRule(Value);
            return string.IsNullOrEmpty(ValidationError);
        }

        return true;
    }

    public object GetValue()
    {
        return FieldType switch
        {
            FormFieldType.DatePicker => DateValue,
            FormFieldType.Switch => BoolValue,
            _ => Value
        };
    }

    public void SetValue(object value)
    {
        switch (FieldType)
        {
            case FormFieldType.DatePicker:
                if (value is DateTime dateTime)
                    DateValue = dateTime;
                break;
            case FormFieldType.Switch:
                if (value is bool boolValue)
                    BoolValue = boolValue;
                break;
            default:
                Value = value?.ToString() ?? string.Empty;
                break;
        }
    }
}

public enum FormFieldType
{
    Entry,
    Editor,
    Picker,
    DatePicker,
    Switch
}