namespace Pauser.Logic.Interfaces {
    public interface IFilter {
        bool Enabled { get; set; }
        string Value { get; set; }
    }
}
