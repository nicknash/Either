public interface IEither<Tl, Tr>
{
    U Case<U>(Func<Tl, U> ofLeft, Func<Tr, U> ofRight);
    void Case(Action<Tl> ofLeft, Action<Tr> ofRight);
    IEither<Tl, U> Map<U>(Func<Tr, U> map);
    IEither<Tl, U> Bind<U>(Func<Tr, IEither<Tl, U>> bind);
}
 
public static class Either
{
    private sealed class LeftImpl<Tl, Tr> : IEither<Tl, Tr>
    {
        private readonly Tl value;
 
        public LeftImpl(Tl value)
        {
            this.value = value;
        }
 
        public U Case<U>(Func<Tl, U> ofLeft, Func<Tr, U> ofRight)
        {
            if (ofLeft == null)
                throw new ArgumentNullException("ofLeft");
 
            return ofLeft(value);
        }
 
        public void Case(Action<Tl> ofLeft, Action<Tr> ofRight)
        {
            if (ofLeft == null)
                throw new ArgumentNullException("ofLeft");
 
            ofLeft(value);
        }
 
        public IEither<Tl, U> Map<U>(Func<Tr, U> map)
        {
            if(map == null)
                throw new ArgumentNullException("map");
            return Either.Left<Tl, U>(value);
        }
 
        public IEither<Tl, U> Bind<U>(Func<Tr, IEither<Tl, U>> bind)
        {
            if(bind == null)
                throw new ArgumentNullException("bind");
            return Either.Left<Tl, U>(value);
        }
        
    }
 
    private sealed class RightImpl<Tl, Tr> : IEither<Tl, Tr>
    {
        private readonly Tr value;
 
        public RightImpl(Tr value)
        {
            this.value = value;
        }
 
        public U Case<U>(Func<Tl, U> ofLeft, Func<Tr, U> ofRight)
        {
            if (ofRight == null)
                throw new ArgumentNullException("ofRight");
            return ofRight(value);
        }
 
        public void Case(Action<Tl> ofLeft, Action<Tr> ofRight)
        {
            if (ofRight == null)
                throw new ArgumentNullException("ofRight");
            ofRight(value);
        }
        public IEither<Tl, U> Map<U>(Func<Tr, U> map)
        {
            if(map == null)
                throw new ArgumentNullException("map");
            return Either.Right<Tl, U>(map(value));
        }
 
        public IEither<Tl, U> Bind<U>(Func<Tr, IEither<Tl, U>> bind)
        {
            if(bind == null)
                throw new ArgumentNullException("bind");
            return bind(value);
        }
    }
    
    public static IEither<Tl, Tr> Left<Tl, Tr>(Tl value)
    {
        return new LeftImpl<Tl, Tr>(value);
    }
 
    public static IEither<Tl, Tr> Right<Tl, Tr>(Tr value)
    {
        return new RightImpl<Tl, Tr>(value);
    }
}