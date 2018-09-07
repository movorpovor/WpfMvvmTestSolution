namespace InvendTest.Generators
{
    enum GeneratedType
    {
        stringType,
        intType
    }

    class GeneratedResult
    {
        public string[] Lines { get; set; }
        public GeneratedType Type { get; set; }
    }
}