using InvendTest.Generators;
using MyGenericLinkedList;
using System.Collections.Generic;
using System.ComponentModel;

namespace InvendTest
{
    class InvendTestModel : INotifyPropertyChanged
    {
        private Dictionary<GeneratedType, IGenerator> _generators = new Dictionary<GeneratedType, IGenerator>();
        private GeneratedType _currentMode;
        private IGenerator _currentGenerator;

        public MyGenericLinkedList<GeneratedResult> ResultHistory = new MyGenericLinkedList<GeneratedResult>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void GenerateNextItems () => ResultHistory.Add(_currentGenerator.Generate(3));

        public void SetPreviousValue()
        {
            GeneratedResult prevNode;
            do
            {
                prevNode = ResultHistory.PenultimateItem;
                ResultHistory.RemoveLast();
            }
            while (ResultHistory.FirstItem != ResultHistory.LastItem && prevNode.Type != _currentMode);

            if (prevNode != null && prevNode.Type != _currentMode)
                ResultHistory.RemoveLast();
        }

        public GeneratedType CurrentMode
        {
            get => _currentMode;
            set
            {
                if (_currentMode != value)
                {
                    _currentMode = value;
                    _currentGenerator = _generators[_currentMode];
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentMode"));
                }
            }
        }

        public InvendTestModel()
        {
            _generators.Add(GeneratedType.intType, new IntGenerator());
            _generators.Add(GeneratedType.stringType, new StringGenerator());
            _currentGenerator = _generators[CurrentMode];
        }
    }
}