using System.Collections.Generic;

namespace WebStone.Factories
{
    public interface IDeckFactory<T>
    {
        T Create(IEnumerable<string> cardIds);
    }
}