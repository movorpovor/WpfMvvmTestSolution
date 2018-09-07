using InvendTest.Commands;
using InvendTest.Generators;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace InvendTest
{
    class InvendTestVM : INotifyPropertyChanged
    {
        public class GenerateResultToView:INotifyPropertyChanged
        {
            private bool _needToBeHighlighted;
            private string _text;
            private GeneratedType _type;

            public event PropertyChangedEventHandler PropertyChanged;

            public bool NeedToBeHighlighted
            {
                get => _needToBeHighlighted;
                set
                {
                    if (value != _needToBeHighlighted)
                    {
                        _needToBeHighlighted = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NeedToBeHighlighted"));
                    }
                }
            }
            public string Text
            {
                get => _text;
                set
                {
                    if (_text != value)
                    {
                        _text = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
                    }
                }
            }
            public GeneratedType Type
            {
                get => _type;
                set
                {
                    if (_type != value)
                    {
                        _type = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
                    }
                }
            }
        }

        private InvendTestModel _model = new InvendTestModel();

        public event PropertyChangedEventHandler PropertyChanged;
        public Command ChangeModeCommand { get; private set; }
        public Command GenerateStringListCommand { get; private set; }
        public Command SetPreviousValueCommand { get; private set; }
        public ObservableCollection<GenerateResultToView> ListOfResults { get; } = new ObservableCollection<GenerateResultToView>();

        public InvendTestVM()
        {
            ChangeModeCommand = new Command(SwitchMode, param => true);
            GenerateStringListCommand = new Command(GenerateStringList, param => true);
            SetPreviousValueCommand = new Command(SetPreviousValue, param => true);
            _model.PropertyChanged += _model_PropertyChanged;
            _model.ResultHistory.ListChanged += ResultHistory_ListChanged; ;
        }

        private void ResultHistory_ListChanged(MyGenericLinkedList.ListChangedEventArgs args)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => HandleChanges(args)));
        }

        private void HandleChanges(MyGenericLinkedList.ListChangedEventArgs args)
        {
            switch (args.Action)
            {
                case MyGenericLinkedList.ListChangedAction.ItemAdded:
                case MyGenericLinkedList.ListChangedAction.ItemRemoved:
                    SetGeneratedResult(args.LastItem as GeneratedResult);
                    break;
            }
        }

        private void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentMode")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IntGlowVisibility"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StringGlowVisibility"));
            }
        }

        public Visibility IntGlowVisibility => _model.CurrentMode == Generators.GeneratedType.intType? Visibility.Visible : Visibility.Hidden;
        public Visibility StringGlowVisibility => _model.CurrentMode == Generators.GeneratedType.stringType ? Visibility.Visible : Visibility.Hidden;

        private void SwitchMode(object mode)
        {
            switch (mode.ToString())
            {
                case "Int":
                    _model.CurrentMode = Generators.GeneratedType.intType;
                    break;
                case "String":
                    _model.CurrentMode = Generators.GeneratedType.stringType;
                    break;
                default:
                    throw new System.NotSupportedException();
            }
        }

        private void GenerateStringList(object mode) => _model.GenerateNextItems();

        private void SetPreviousValue(object mode) => _model.SetPreviousValue(); 

        private void SetGeneratedResult(GeneratedResult result)
        {
            if (result == null)
                ListOfResults.Clear();
            else
            {
                GenerateResultToView[] linesToView = null;

                switch (result.Type)
                {
                    case GeneratedType.intType:
                        linesToView = SetIntResult(result.Lines);
                        break;
                    case GeneratedType.stringType:
                        linesToView = SetStringResult(result.Lines);
                        break;
                    default:
                        throw new NotSupportedException();
                }

                for (int i = 0; i < linesToView.Length; i++)
                {
                    if (ListOfResults.Count < i + 1)
                        ListOfResults.Add(linesToView[i]);
                    else
                    {
                        ListOfResults[i].Text = linesToView[i].Text;
                        ListOfResults[i].Type = linesToView[i].Type;
                        ListOfResults[i].NeedToBeHighlighted = linesToView[i].NeedToBeHighlighted;
                    }
                }
            }
        }

        private GenerateResultToView[] SetIntResult(string[] lines)
        {
            var maxInt = lines.Max(x => int.Parse(x)).ToString();
            return lines.Select(x => new GenerateResultToView()
            {
                NeedToBeHighlighted = x == maxInt,
                Text = $"INT:{x}",
                Type = GeneratedType.intType
            }).ToArray();
        }

        private GenerateResultToView[] SetStringResult(string[] lines)
        {
            return lines.Select(x => new GenerateResultToView()
            {
                Text = $"STR:{x}",
                Type = GeneratedType.stringType,
                NeedToBeHighlighted = x.Distinct().Count() < x.Length
            }).ToArray();
        }
    }
}